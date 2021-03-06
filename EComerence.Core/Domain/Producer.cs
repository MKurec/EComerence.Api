﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EComerence.Core.Domain
{
    public class Producer : Entity
    {
        public string Name { get; protected set; }
        public Producer(Guid id, string name)
        {
            Id = id;
            SetName(name);
        }
        public void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new Exception($"Category with id'{Id}' cannot have empty name");
            }
            Name = name;
        }
    }
}
