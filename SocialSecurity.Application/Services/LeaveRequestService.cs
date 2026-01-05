using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SocialSecurity.Application.Interfaces;
using SocialSecurity.Application.UnitOfWorks;
using SocialSecurity.Domain.Models;
using SocialSecurity.Domain.Models.Common;
using SocialSecurity.Infrastructure.Integrations;
using SocialSecurity.Shared.Dtos.Common;
using SocialSecurity.Shared.Dtos.Holiday;

namespace SocialSecurity.Application.Services
{
    public class LeaveRequestService : ILeaveRequestService
    {
        private readonly IRepository<LeaveRequest> _leaveRequestRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserServiceClient _userServiceClient;

        public LeaveRequestService(
            IRepository<LeaveRequest> leaveRequestRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IUserServiceClient userServiceClient)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userServiceClient = userServiceClient;
        }

        /// <summary>
        /// This method retrieves statistics about leave requests, including total requests, approved requests, pending requests, and rejected requests.
        /// </summary>
        /// <returns></returns>
        public async Task<Response> GetStatsAsync()
        {
            // Use IQueryable to build queries efficiently
            var query = _leaveRequestRepository.GetQueryable(asNoTracking: true);

            // Execute counts in parallel
            var totalRequestsTask = await query.CountAsync();
            var approvedRequestsTask = await query.Where(r => r.Status == LeaveStatusEnum.Approved.ToString() && r.IsActive && !r.IsDeleted).CountAsync();
            var pendingRequestsTask = await query.Where(r => r.Status == LeaveStatusEnum.Pending.ToString() && r.IsActive && !r.IsDeleted).CountAsync();
            var rejectedRequestsTask = await query.Where(r => r.Status == LeaveStatusEnum.Rejected.ToString() && r.IsActive && !r.IsDeleted).CountAsync();


            var stats = new
            {
                TotalRequests = totalRequestsTask,
                ApprovedRequests = approvedRequestsTask,
                PendingRequests = pendingRequestsTask,
                RejectedRequests = rejectedRequestsTask
            };

            return new Response
            {
                Status = ResponseSatusEnums.Success.ToString(),
                Message = "Leave request statistics retrieved successfully.",
                Data = stats,
                Code = 200
            };
        }

        /// <summary>
        /// Creates a new leave request entry in the database.
        /// </summary>
        public async Task<Response> CreateLeaveRequestAsync(LeaveRequestDto leaveRequestDto)
        {
            var user = await _userServiceClient.GetUserAsync(leaveRequestDto.UserId);

            if (user == null)
            {
                return new Response
                {
                    Status = ResponseSatusEnums.Error.ToString(),
                    Message = "User not found.",
                    Code = 404
                };
            }

            var leaveRequest = _mapper.Map<LeaveRequest>(leaveRequestDto);
            leaveRequest.RequestId = await GenerateRequestIdAsync();
            var result = await _leaveRequestRepository.AddAsync(leaveRequest);
            await _unitOfWork.SaveAsync();

            var resultDto = _mapper.Map<LeaveRequestDto>(result);
            return new Response
            {
                Status = ResponseSatusEnums.Success.ToString(),
                Message = "Leave request created successfully.",
                Data = resultDto,
                Code = 200
            };
        }

        /// <summary>
        /// Retrieves all leave request entries from the database.
        /// </summary>
        public async Task<Response> GetLeaveRequestsAsync()
        {
            var leaveRequests = await _leaveRequestRepository.GetAllAsync();
            var leaveRequestDtos = _mapper.Map<IEnumerable<LeaveRequestDto>>(leaveRequests);

            return new Response
            {
                Status = ResponseSatusEnums.Success.ToString(),
                Message = "Leave requests retrieved successfully.",
                Data = leaveRequestDtos,
                Code = 200
            };
        }

        /// <summary>
        /// Retrieves a leave request entry by its ID.
        /// </summary>
        public async Task<Response> GetLeaveRequestByIdAsync(long id)
        {
            var leaveRequest = await _leaveRequestRepository.GetAsync((int)id);
            if (leaveRequest == null)
            {
                return new Response
                {
                    Status = ResponseSatusEnums.Error.ToString(),
                    Message = "Leave request not found.",
                    Code = 404
                };
            }

            var leaveRequestDto = _mapper.Map<LeaveRequestDto>(leaveRequest);
            return new Response
            {
                Status = ResponseSatusEnums.Success.ToString(),
                Message = "Leave request retrieved successfully.",
                Data = leaveRequestDto,
                Code = 200
            };
        }

        /// <summary>
        /// Updates an existing leave request entry.
        /// </summary>
        public async Task<Response> UpdateLeaveRequestAsync(int id, LeaveRequestDto leaveRequestDto)
        {
            var existingLeaveRequest = await _leaveRequestRepository.GetAsync(id);
            if (existingLeaveRequest == null)
            {
                return new Response
                {
                    Status = ResponseSatusEnums.Error.ToString(),
                    Message = "Leave request not found.",
                    Code = 404
                };
            }

            _mapper.Map(leaveRequestDto, existingLeaveRequest);
            existingLeaveRequest.ModifiedOn = DateTime.UtcNow;

            _leaveRequestRepository.Update(existingLeaveRequest);
            _unitOfWork.Save();

            var updatedDto = _mapper.Map<LeaveRequestDto>(existingLeaveRequest);
            return new Response
            {
                Status = ResponseSatusEnums.Success.ToString(),
                Message = "Leave request updated successfully.",
                Data = updatedDto,
                Code = 200
            };
        }

        /// <summary>
        /// Deletes a leave request entry by its ID.
        /// </summary>
        public async Task<Response> DeleteLeaveRequestAsync(long id)
        {
            var leaveRequest = await _leaveRequestRepository.GetAsync((int)id);
            if (leaveRequest == null)
            {
                return new Response
                {
                    Status = ResponseSatusEnums.Error.ToString(),
                    Message = "Leave request not found.",
                    Code = 404
                };
            }

            _leaveRequestRepository.Remove(leaveRequest);
            await _unitOfWork.SaveAsync();

            return new Response
            {
                Status = ResponseSatusEnums.Success.ToString(),
                Message = "Leave request deleted successfully.",
                Code = 200
            };
        }

        /// <summary>
        /// This method allows audit manager to approve or reject a leave request based on its ID and the provided decision.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="decision"></param>
        /// <returns></returns>
        public async Task<Response> DecideLeaveRequestAsync(int id, string decision)
        {
            var leaveRequest = await _leaveRequestRepository.GetAsync(id);
            if (leaveRequest == null)
            {
                return new Response
                {
                    Code = 404,
                    Status = "Error",
                    Message = "Leave request not found."
                };
            }

            if (string.IsNullOrWhiteSpace(decision))
            {
                return new Response
                {
                    Code = 400,
                    Status = "Error",
                    Message = "Decision is required."
                };
            }

            // Try to parse the decision into enum
            if (!Enum.TryParse(typeof(LeaveStatusEnum), decision, true, out var parsedStatus))
            {
                return new Response
                {
                    Code = 400,
                    Status = "Error",
                    Message = $"Invalid decision '{decision}'. Allowed values are: {string.Join(", ", Enum.GetNames(typeof(LeaveStatusEnum)))}"
                };
            }

            var newStatus = (LeaveStatusEnum)parsedStatus;

            // Check current status - convert string in DB to enum for validation
            if (!Enum.TryParse(typeof(LeaveStatusEnum), leaveRequest.Status, true, out var currentParsedStatus))
            {
                return new Response
                {
                    Code = 500,
                    Status = "Error",
                    Message = "Invalid current status stored in database."
                };
            }

            var currentStatus = (LeaveStatusEnum)currentParsedStatus;

            if (currentStatus != LeaveStatusEnum.Pending)
            {
                return new Response
                {
                    Code = 400,
                    Status = "Error",
                    Message = $"This leave request has already been {leaveRequest.Status}."
                };
            }

            if (newStatus != LeaveStatusEnum.Approved && newStatus != LeaveStatusEnum.Rejected)
            {
                return new Response
                {
                    Code = 400,
                    Status = "Error",
                    Message = "Only 'Approved' or 'Rejected' decisions are allowed."
                };
            }

            // Save the enum name as string in DB
            leaveRequest.Status = newStatus.ToString();
            leaveRequest.ModifiedOn = DateTime.UtcNow;

            _leaveRequestRepository.Update(leaveRequest);
            await _unitOfWork.SaveAsync();

            var updatedDto = _mapper.Map<LeaveRequestDto>(leaveRequest);

            return new Response
            {
                Code = 200,
                Status = "Success",
                Message = $"Leave request has been {leaveRequest.Status.ToLower()} successfully.",
                Data = updatedDto
            };
        }


        /// <summary>
        /// This method generates a unique request ID for a new leave request in the format "LR-{Year}-{SequentialNumber}".
        /// </summary>
        /// <returns></returns>
        private async Task<string> GenerateRequestIdAsync()
        {
            var currentYear = DateTime.UtcNow.Year;

            // Get the latest LeaveRequest to determine the next sequence number
            var lastRequest = (await _leaveRequestRepository.GetAllAsync())
                                .OrderByDescending(r => r.CreatedOn)
                                .FirstOrDefault();

            int nextNumber = 1;

            if (lastRequest != null && !string.IsNullOrEmpty(lastRequest.RequestId))
            {
                var parts = lastRequest.RequestId.Split('-');  // LR-2025-001
                if (parts.Length == 3 && int.TryParse(parts[2], out int lastNumber))
                {
                    nextNumber = lastNumber + 1;
                }
            }

            return $"LR-{currentYear}-{nextNumber:D3}";
        }

    }
}
