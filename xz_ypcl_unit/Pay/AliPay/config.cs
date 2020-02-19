using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xz_ypcl_unit.Pay.AliPay
{
    public class config
    {
        // 应用ID,您的APPID
        public static string app_id = "2021001128610679";

        // 支付宝网关
        public static string gatewayUrl = "https://openapi.alipay.com/gateway.do";

        // 商户私钥，您的原始格式RSA私钥
        public static string private_key = @"MIIEowIBAAKCAQEAsbqKA+iI9iWAzFkAfGdKeNqrx4X3pfUX7xEXpqqM8fDaKyJPYofHMSjMr+QgaNUlpswtwJU8SdQpyQ5RflrlvuhHCYVnyS99F+5C+YEKzfouTgQ5rIwHkosVOMURz7YQOqEOoLu8Pmkq2xFb4F+3n4d0m8KcGkOGEsb8RC/YeFRSHawWuR2E3hg3vBqN5UOozcJLiykVp5+dDxk9qenJbmnKp8n240BpIS8PPuWIP3/Fa3dMTlzsUCKaBPL43GbFfHq9/MYo5FGFRXRoJTOu0erv3x9I+5uoCBtzCcQ/BDdtpagdjBbKD2zxh9n7nSqLvSw2JOaUhY2fb5yC90nzKQIDAQABAoIBAQCpUE5fPs4LwNqc0kU2PqihzaBMagHtJjrw01W2v0+axZrx8LOz6CoJWRb2kduWQ0ilSGplx0YyB17vXSahuzKX1mymU2L2NLl1bNR9IUQLykUGqdvP1273WmyTMpqvSIDZecmXsoG46zOak0T10fn6jm62XIWeN6mohcyZoQMWTZPBpp9Pqg104Tqf1OPiRoXzisfOxflHPdAs0Yon4pC3DR6kqCUcQIWUmrHQyJ7Rh7xs969ZMqs70yjEqlCT/nbsF4npZ+jIJPNivvaJICHkAgtiwQMw8h+zh+4BegbWK/lbKuGvhAjt++5TcUg6SFX5SDH1J6tQyqGACAU+W3GJAoGBAPUEp1jNvkvdHJ39kwwKRkIpG5TR7LPWPemUF6xZz/M4uHJOAhzRuTXc4JdQhcCK0VvTBbQdl0OBnVJ40xw8cnGMpAZehQXLxCRDCj6JY2zc1/Z1p9Ohguzi+137gGoWAlAH/CP1WnvcEenjwTjnPQKjMktBCA1cm5bqC7bSPR5bAoGBALmxza/Yjda9XZWGVyQTe4azBAo5eor68Cg7Dvz1BX0LqCdHZD1HRtmx0ScIpbNXykdMJPMoJRronyiIfs3QK4XT0LCUAR0gJ8ZSVj7+3+7iUckdcePwIWeZHfGbUPF+GeQ05Basx7PbHOqx+2dQ6wRTfrbE9mXt0aO1VAZ7kHPLAoGASzelhB6NN87mVR4eQUj6Lp9eBz62srKniQhciB1+OEWGYhjOjzhW6lEnZWxj0ysKgGz0yEl5QVEOEd5juLxZycAKLqZfodrfGN63y2Dz89yMM+4EmnRvs/cugbiOwIKHLTh2UDenEnUJzsqLa8OZODEPZIjPRwLSBO9Or041UxcCgYBqtyrstrf0w8jnaFfl7Khdpb1ZnuS0wDeJ9z7K0oj/7tYJFLcfnDm0W8NF+ms3oOknhjPp5ZVFXJAy/BZbcxnABBV3lOb69QLGr4TPGSxaHYhA2wIbq2GXJuCVe9vNnNmGU3sNKOhvjSmWGwjOvCsDxnQY1yJ/O2fx/AA45YQDdwKBgCakV6FmiNrTX1kAimwy79pxtUASir/Mq/iGwTljyTHv2ZERF8DQ6Ry0ZIY/jybZfRDKSI2twNfHsJy8ZpZ/oAJRqV/PRkHWXsw1bE3HvakNQl1R/PsYwd73bpd/tN8bnRFcGGFpCz3wc+Ik2sbYFkHdhp2yF1XdxWShbcNJ89Ze";

        // 支付宝公钥,查看地址：https://openhome.alipay.com/platform/keyManage.htm 对应APPID下的支付宝公钥。
        public static string alipay_public_key = @"MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAsbqKA+iI9iWAzFkAfGdKeNqrx4X3pfUX7xEXpqqM8fDaKyJPYofHMSjMr+QgaNUlpswtwJU8SdQpyQ5RflrlvuhHCYVnyS99F+5C+YEKzfouTgQ5rIwHkosVOMURz7YQOqEOoLu8Pmkq2xFb4F+3n4d0m8KcGkOGEsb8RC/YeFRSHawWuR2E3hg3vBqN5UOozcJLiykVp5+dDxk9qenJbmnKp8n240BpIS8PPuWIP3/Fa3dMTlzsUCKaBPL43GbFfHq9/MYo5FGFRXRoJTOu0erv3x9I+5uoCBtzCcQ/BDdtpagdjBbKD2zxh9n7nSqLvSw2JOaUhY2fb5yC90nzKQIDAQAB";

        // 签名方式
        public static string sign_type = "RSA2";

        // 编码格式
        public static string charset = "UTF-8";
    }
}
