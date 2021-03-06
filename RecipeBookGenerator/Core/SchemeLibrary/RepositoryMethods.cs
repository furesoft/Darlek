﻿using BookGenerator.Core.RuntimeLibrary;

namespace BookGenerator.Core.SchemeLibrary
{
    [RuntimeType("runtime")]
    public static class RepositoryMethods
    {
        [RuntimeMethod("repository-get-metadata")]
        public static object GetMetadata(string key)
        {
            return Repository.GetMetadata(key);
        }
    }
}