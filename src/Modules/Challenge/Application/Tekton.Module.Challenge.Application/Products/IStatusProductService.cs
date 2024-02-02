using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekton.Module.Challenge.Domain.Products;

namespace Tekton.Module.Challenge.Application.Products
{
    public interface IStatusProductService
    {
        Task<string> GetStatusName(int status);
    }
}
