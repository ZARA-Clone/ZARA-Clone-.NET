using FluentValidation.Results;

namespace E_CommerceProject.Business.Shared
{
    public class ServiceResponse
    {
        private List<string> _errors = new List<string>();
        public List<string> Errors => _errors;
        public bool IsSuccess
        {
            get { return _errors == null || !_errors.Any(); }
        }
        protected ServiceResponse()
        {

        }

        public static ServiceResponse Success()
        {
            return new ServiceResponse();
        }
        public static ServiceResponse Fail(IEnumerable<ValidationFailure> errors)
        {
            ServiceResponse response = new ServiceResponse();
            foreach (var error in errors)
            {
                response.AddError(error.ErrorMessage);
            }
            return response;
        }
        public void AddError(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentException("message cannot be null or whitespace");
            _errors.Add(message);
        }

    }

    public class ServiceResponse<T> : ServiceResponse
    {

        public T Data { get; private set; }
        public ServiceResponse(T data) : base()
        {
            Data = data;
        }
        public static ServiceResponse<T> Success(T data)
        {
            ServiceResponse<T> response = new ServiceResponse<T>(data);
            return response;
        }

        public static ServiceResponse<T> Fail(List<ValidationFailure> errors, T data)
        {
            ServiceResponse<T> response = new ServiceResponse<T>(data);
            foreach (var error in errors)
            {
                response.AddError(error.ErrorMessage);
            }
            return response;
        }
    }
}
