namespace EscolaDeIdiomas.Utils
{
    public static class StringExtension
    {
        public static bool ValidaCPF(this string cpf)
        {
            bool cpfValido = true;

            if (cpf.Length != 11) // Verificar se tem 11 digitos
            {
                cpfValido = false;
            }
            else
            {
                for (int i = 0; i < cpf.Length; i++) // Verificar se todos os caracteres de cpf são digitos numéricos
                {
                    if (!Char.IsDigit(cpf[i]))
                    {
                        cpfValido = false;
                        break;
                    }
                }
            }

            if (cpfValido) // Verificar se é 00000000000, ..., 99999999999
            {
                for (byte i = 0; i < 10; i++)
                {
                    var temp = new string(Convert.ToChar(i), 11);
                    if (cpf == temp)
                    {
                        cpfValido = false;
                        break;
                    }
                }
            }

            if (cpfValido) // Verificar digito de controle do cpf
            {
                var j = 0;
                var d1 = 0;
                var d2 = 0;

                for (int i = 10; i > 1; i--) // Validar o primeiro número do digito de controle
                {
                    d1 += Convert.ToInt32(cpf.Substring(j, 1)) * i;
                    j++;
                }

                d1 = (d1 * 10) % 11; // Resto da divisão
                if (d1 == 10)
                    d1 = 0;

                if (d1 != Convert.ToInt32(cpf.Substring(9, 1))) // Verificar se o primeiro número é válido ---- posição 9 (penultima)
                    cpfValido = false;

                if (cpfValido) // Validar o segundo número do digito de controle
                {
                    j = 0;
                    for (int i = 11; i > 1; i--)
                    {
                        d2 += Convert.ToInt32(cpf.Substring(j, 1)) * i;
                        j++;
                    }

                    d2 = (d2 * 10) % 11; // Resto da divisão
                    if (d2 == 10)
                        d2 = 0;

                    if (d2 != Convert.ToInt32(cpf.Substring(10, 1))) // Verificar se o segundo número é válido ---- posição 10 (ultima)
                        cpfValido = false;
                }
            }

            return cpfValido;
        } // Um validador de CPF
        public static bool ValidaEmail(this string email) // Um validador de Email
        {
            try
            {
                var enderecoEmail = new System.Net.Mail.MailAddress(email);
                return enderecoEmail.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
