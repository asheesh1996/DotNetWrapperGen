﻿using System;

namespace DotNetWrapperGen.CodeModel
{
    public class PropertyDefinition : ModelNodeDefinition
    {
        public MethodDefinition Getter { get; private set; }
        public MethodDefinition Setter { get; set; }

        public TypeRefDefinition Type
        {
            get
            {
                return Getter.ReturnType;
            }
        }

        public PropertyDefinition(MethodDefinition getter, string name)
            : base(name)
        {
            Getter = (MethodDefinition)getter.ClonedFrom;
            Parent = (ClassDefinition)getter.Parent;
            Parent.Children.Add(this);
            getter.Property = this;
        }

        // Property from field
        public PropertyDefinition(FieldDefinition field, string name)
            : base(name)
        {
            //Name = field.ManagedName;
            Parent = field.Parent as ClassDefinition;
            throw new NotImplementedException();
            //Parent.Properties.Add(this);
            /*
            string name = field.Name;
            if (name.StartsWith("m_"))
            {
                name = name.Substring(2);
            }
            name = name.Substring(0, 1).ToUpper() + name.Substring(1); // capitalize

            // one_two_three -> oneTwoThree
            while (name.Contains("_"))
            {
                int pos = name.IndexOf('_');
                name = name.Substring(0, pos) + name.Substring(pos + 1, 1).ToUpper() + name.Substring(pos + 2);
            }

            VerblessName = name;

            // Generate getter/setter methods
            string getterName, setterName;
            if (name.StartsWith("has"))
            {
                getterName = name;
                setterName = "set" + name.Substring(3);
            }
            else if (name.StartsWith("is"))
            {
                getterName = name;
                setterName = "set" + name.Substring(2);
            }
            else
            {
                getterName = "get" + name;
                setterName = "set" + name;
            }

            Getter = new MethodDefinition(getterName);
            Getter.ReturnType = field.Type;
            Getter.Field = field;
            Getter.Property = this;
            Parent.AddChild(Getter);

            Setter = new MethodDefinition(setterName, new ParameterDefinition("value", field.Type));
            Setter.ReturnType = new TypeRefDefinition();
            Setter.Field = field;
            Setter.Property = this;
            Parent.AddChild(Setter);*/
        }

        public override object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
