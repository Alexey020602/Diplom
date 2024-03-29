﻿using DataBase.Models;

namespace Client.Services;

public interface IDirectionsService
{
    Task<List<Direction>> GetDirections();
    Task<Direction> GetDirection(int id);
    Task DeleteDirection(int id);
    Task UpdateDirection(Direction direction);
    Task AddDirection(Direction direction);
}
