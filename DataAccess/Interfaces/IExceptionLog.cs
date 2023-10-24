using System;
using System.Collections.Generic;
using DataAccess;
namespace DataAccess.Interfaces
{
    interface IExceptionLog
    {
        bool AddException(ExceptionLog exception);
        bool UpdateException(ExceptionLog exception);
        bool DeleteException(int Id);
        ExceptionLog GetExceptionById(int id);
        List<ExceptionLog> GetEXceptionsListByCode(int ExceptionCode);
    }
}
