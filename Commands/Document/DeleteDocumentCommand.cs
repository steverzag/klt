using FloraYFaunaAPI.Validations;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FloraYFaunaAPI.Commands.Document
{
    public class DeleteDocumentCommand
    {
        [ValidationGuid]
        public Guid Id { get; set; }
    }
}
