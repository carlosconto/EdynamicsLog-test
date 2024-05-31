﻿using MediatR;

namespace Application.Users.GetUsers;

public record GetUsersQuery : IRequest<IEnumerable<UserResponse>>;