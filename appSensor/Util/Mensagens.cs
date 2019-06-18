using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace appSensor.Util
{
    public static class Mensagens
    {

        public enum Mensagem
        {
            [StringValue("Houve um Erro na Execução")]
            HOUVEUMERRONAEXECUCAO,
            [StringValue("Registro incluido com sucesso !")]
            INCLUIDO_SUCESSO,
            [StringValue("Registro excluido com sucesso !")]
            EXCLUIDO_SUCESSO,
            [StringValue("Registro alterado com sucesso !")]
            ALTERADO_SUCESSO,
            [StringValue("Registro não encontrado  !")]
            NAO_ENCONTRADO
        }

        public static string GetStringValue(this Enum value)
        {
            string stringValue = value.ToString();
            Type type = value.GetType();
            FieldInfo fieldInfo = type.GetField(value.ToString());
            StringValue[] attrs = fieldInfo.
                GetCustomAttributes(typeof(StringValue), false) as StringValue[];
            if (attrs.Length > 0)
            {
                stringValue = attrs[0].Value;
            }
            return stringValue;
        }


        public static string GetMensagem(string mg, Boolean action = false)
        {
            foreach (Mensagem mc in Enum.GetValues(typeof(Mensagem)))
            {
                if (mc.ToString() == mg)
                {
                    return GetStringValue(mc);
                }
            }
            if (action)
            {
                return mg;
            }
            return null;
        }

    }

    [Serializable]
    public class StringValue : Attribute
    {
        public string Value { get; private set; }

        public StringValue(string value)
        {
            Value = value;
        }
    }
}