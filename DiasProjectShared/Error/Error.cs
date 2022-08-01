using System;
using System.Collections.Generic;

namespace DiasShared.Errors
{
    public sealed class Error : ValueObject
    {
        private const string Separator = "||";

        /// <summary>
        /// unique olacak
        /// </summary>
        public string Code { get; }

        public string Message { get; }

        public string DisplayMessage { get; set; }

        public bool BusinessOperationSucceed { get; } = false;

        public Error(string code, string message,string displayMessage, bool businessOperationSucceed = false)
        {
            Code = code;
            Message = message;
            DisplayMessage = displayMessage;
            BusinessOperationSucceed = businessOperationSucceed;
        }
        public string Serialize()
        {
            return $"{Code}{Separator}{Message}";
        }

        public static Error Deserialize(string serialized)
        {
            string[] data = serialized.Split(new[] { Separator }, StringSplitOptions.RemoveEmptyEntries);
            if (data.Length >= 2)
            {
                return new Error(code: "desarialize.error", "can not desarialize error structer", "");
            }

            return new Error(data[0], data[1],"");
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Code;
        }
    }
}
