using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface ICommand<TRequest>
    {
        void Execute(TRequest request);
    }

    public interface ICommand<TRequest, TResult>
    {
        TResult Execute(TRequest request);
    }

    public interface ICommand<TRequest, TRequest2, TResult>
    { 
        void Execute(TRequest request, TRequest2 request2);
    }
}
