﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekton.Module.Challenge.Application
{
    public  class OperationResult<T>: OperationResult
    {
        public OperationResult(T data, bool success): base(success)
        {
            Data = data;
        }

        public T Data { get; }
    }
}
