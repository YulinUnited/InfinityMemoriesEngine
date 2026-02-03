using System.Globalization;
using System.Reflection;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Objects.Types
{
    internal partial class MethodInpto : MemberInfo, IReflect
    {
        private readonly Type _targetType;

        public MethodInpto(Type type)
        {
            _targetType = type ?? throw new ArgumentNullException(nameof(type));
            UnderlyingSystemType = type;
        }

        public new Type UnderlyingSystemType { get; set; }

        public override Type? DeclaringType => _targetType.DeclaringType;
        public override string Name => _targetType.Name;
        public override Type? ReflectedType => _targetType;
        public override MemberTypes MemberType => MemberTypes.TypeInfo;

        public FieldInfo? GetField(string name, BindingFlags bindingAttr)
            => _targetType.GetField(name, bindingAttr);

        public FieldInfo[] GetFields(BindingFlags bindingAttr)
            => _targetType.GetFields(bindingAttr);

        public MemberInfo[] GetMember(string name, BindingFlags bindingAttr)
            => _targetType.GetMember(name, bindingAttr);

        public MemberInfo[] GetMembers(BindingFlags bindingAttr)
            => _targetType.GetMembers(bindingAttr);

        public MethodInfo? GetMethod(string name, BindingFlags bindingAttr)
            => _targetType.GetMethod(name, bindingAttr);

        public MethodInfo? GetMethod(string name, BindingFlags bindingAttr, Binder? binder, Type[] types, ParameterModifier[]? modifiers)
            => _targetType.GetMethod(name, bindingAttr, binder, types, modifiers);

        public MethodInfo[] GetMethods(BindingFlags bindingAttr)
            => _targetType.GetMethods(bindingAttr);

        public PropertyInfo[] GetProperties(BindingFlags bindingAttr)
            => _targetType.GetProperties(bindingAttr);

        public PropertyInfo? GetProperty(string name, BindingFlags bindingAttr)
            => _targetType.GetProperty(name, bindingAttr);

        public PropertyInfo? GetProperty(string name, BindingFlags bindingAttr, Binder? binder, Type? returnType, Type[] types, ParameterModifier[]? modifiers)
            => _targetType.GetProperty(name, bindingAttr, binder, returnType, types, modifiers);

        public object? InvokeMember(string name, BindingFlags invokeAttr, Binder? binder, object? target, object?[]? args, ParameterModifier[]? modifiers, CultureInfo? culture, string[]? namedParameters)
            => _targetType.InvokeMember(name, invokeAttr, binder, target, args ?? Array.Empty<object>(), modifiers ?? Array.Empty<ParameterModifier>(), culture, namedParameters ?? Array.Empty<string>());

        public override object[] GetCustomAttributes(bool inherit)
            => _targetType.GetCustomAttributes(inherit);

        public override object[] GetCustomAttributes(Type attributeType, bool inherit)
            => _targetType.GetCustomAttributes(attributeType, inherit);

        public override bool IsDefined(Type attributeType, bool inherit)
            => _targetType.IsDefined(attributeType, inherit);

    }
}
