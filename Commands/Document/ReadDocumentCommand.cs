using FloraYFaunaAPI.Validations;
using System;
using System.Text.Json.Serialization;

namespace FloraYFaunaAPI.Commands.Document
{
    public class ReadDocumentCommand
    {
        [ValidationGuid]
        public Guid Id { get; set; }
    }
}
