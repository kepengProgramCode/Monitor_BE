using System.ComponentModel;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace Monitor_BE.Common
{
    public class Fun
    {
        public static string GetEnumDescription(Enum enumValue)
        {
            string value = enumValue.ToString();
            FieldInfo? field = enumValue.GetType().GetField(value);
            object[] objs = field.GetCustomAttributes(typeof(DescriptionAttribute), false);  //获取描述属性
            if (objs == null || objs.Length == 0)  //当描述属性没有时，直接返回名称
                return value;
            DescriptionAttribute descriptionAttribute = (DescriptionAttribute)objs[0];
            return descriptionAttribute.Description;
        }


        public static string md5Encrypt(string str)
        {
            MD5 md5 = MD5.Create();
            byte[] buffer = Encoding.UTF8.GetBytes(str);//将字符串转成字节数组
            byte[] byteArray = md5.ComputeHash(buffer);//调用加密方法
            StringBuilder sb = new();
            foreach (byte b in byteArray)//遍历字节数组
            {
                sb.Append(b.ToString("x2"));//将字节数组转成16进制的字符串。X表示16进制，2表示每个16字符占2位
            }
            return sb.ToString();
        }

    }
}
