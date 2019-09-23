﻿using FluentValidation;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace SCA.Model
{
    public class Area : Entity
    {
        public string Nome { get; set; }
        public ICollection<Teste> Teste { get; set; }
    }
    public class Teste
    {
        public string Nome { get; set; }
        [BsonIgnoreIfNull]

        public int? MyProperty { get; set; }
        [BsonIgnoreIfNull]
        public string MyProperty2 { get; set; }
    }

    public class AreaValidator : AbstractValidator<Area>
    {
        public AreaValidator()
        {
            RuleFor(x => x.Nome).NotEmpty();
        }
    }
}
