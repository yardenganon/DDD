﻿using DDDProject.Domain;

namespace DDDProject.Providers;

public class GoogleProvider : IProvider
{
    public Task<IReadOnlyCollection<AdContent>> GetAdContents(long account)
    {
        // api call
        
        // map to AdContent
        
        // return
        throw new NotImplementedException();
    }
}