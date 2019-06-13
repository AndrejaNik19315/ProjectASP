using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface ICommand<TRequest>
    {
        void Execute(TRequest request);

        void Execute(TRequest request, int id);
    }

    public interface ICommand<TRequest, TResult>
    {
        TResult Execute(TRequest request);

        TResult Execute(TRequest request, int id);
    }

    //public interface ICommand<TRequest,TRequest2, TResult>
    //{
    //    TResult Execute(TRequest request);

    //    TResult Execute(TRequest request, TRequest2 request2);
    //}
}
