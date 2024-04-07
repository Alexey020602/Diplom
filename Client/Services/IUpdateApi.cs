﻿using Refit;

namespace Client.Services;

public interface IUpdateApi<T, in Id> where T : class
{
    [Put("/{id}")]
    Task Update(Id id, [Body] T payload);
}