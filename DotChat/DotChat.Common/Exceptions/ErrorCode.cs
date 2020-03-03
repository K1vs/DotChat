namespace K1vs.DotChat.Common.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ErrorCode
    {
        public long Code { get; }
        public string Name { get; }

        public ErrorFamily Family { get; }
        public ErrorType Type { get; }
        public ErrorModule Module { get; }
        public ErrorOperation Operation { get; }
        public ErrorScope Scope { get; }
        public ErrorEntity Entity { get; }

        public ErrorCode(ErrorType type, ErrorModule module, ErrorOperation operation, ErrorEntity entity)
        {
            Family = (ErrorFamily)Enum.ToObject(typeof(ErrorFamily),(int)type / 1000);
            Type = type;
            Module = module;
            Operation = operation;
            Scope = (ErrorScope)Enum.ToObject(typeof(ErrorScope), (int)entity / 1000);
            Entity = entity;
            long shift = 1;
            Code = (long) entity + (long) operation * (shift *= 100000) + (long) module * (shift *= 1000) +
                   (long) type * (shift * 100);
            Name = $"{type}{module}{operation}{entity}";
        }

        public ErrorCode(ErrorFamily family, ErrorModule module, ErrorOperation operation, ErrorEntity entity)
        {
            Family = family;
            Type = ErrorType.Unknown;
            Module = module;
            Operation = operation;
            Scope = (ErrorScope)Enum.ToObject(typeof(ErrorScope), (int)entity / 1000);
            Entity = entity;
            long shift = 1;
            Code = (long)entity + (long)operation * (shift *= 100000) + (long)module * (shift *= 1000) +
                   (long)family * (shift * 100);
            Name = $"{family}{module}{operation}{entity}";
        }

        public ErrorCode(ErrorFamily family, ErrorModule module, ErrorOperation operation, ErrorScope scope)
        {
            Family = family;
            Type = ErrorType.Unknown;
            Module = module;
            Operation = operation;
            Scope = scope;
            Entity = ErrorEntity.None;
            long shift = 1;
            Code = (long)scope + (long)operation * (shift *= 100000) + (long)module * (shift *= 1000) +
                   (long)family * (shift * 100000);
            Name = $"{family}{module}{operation}{scope}";
        }

        public ErrorCode(ErrorType type, ErrorModule module, ErrorOperation operation, ErrorScope scope)
        {
            Family = (ErrorFamily)Enum.ToObject(typeof(ErrorFamily), (int)type / 1000);
            Type = ErrorType.Unknown;
            Module = module;
            Operation = operation;
            Scope = scope;
            Entity = ErrorEntity.None;
            long shift = 1;
            Code = (long)scope + (long)operation * (shift *= 100000) + (long)module * (shift *= 1000) +
                   (long)type * (shift * 100);
            Name = $"{type}{module}{operation}{scope}";
        }
    }
}
