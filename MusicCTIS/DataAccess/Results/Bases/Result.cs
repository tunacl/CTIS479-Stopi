using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Results.Bases
{
    public abstract class Result
    {

        public bool isSuccess { get;  }//sadece read only olacak set e gerek yok

        public string Message { get; }


        protected Result(bool isSuccess, string message)
        {
            this.isSuccess = isSuccess;
            Message = message;
        }
    }
}
