namespace CustomerAPI.Util
{
    public class Message
    {
        public const int INVALID_EMAIL= 1;
        public const int INVALID_NAME= 2;
        public const int CUSTOMER_NOT_FOUND = 3;
        public const int REGISTERED_CUSTOMER_SUCCESS = 4;
        public const int REGISTERED_CUSTOMER_ERROR = 5;
        public const int REGISTERED_CUSTOMER_CONCURRENCY_ERROR = 6;
        public const int DELETED_CUSTOMER_SUCCESS = 7;
        public const int DELETED_CUSTOMER_ERROR = 8;


        public static string Text(int message)
        {
            switch (message)
            {
                case INVALID_EMAIL:
                    return "Email inválido!";
                case INVALID_NAME:
                    return "Nome inválido";
                case CUSTOMER_NOT_FOUND:
                    return "Cliente não encontrado!";
                case REGISTERED_CUSTOMER_SUCCESS:
                    return "Cliente salvo com sucesso!";
                case REGISTERED_CUSTOMER_ERROR:
                    return "Já existe um cliente com o mesmo email cadastrado!";
                case REGISTERED_CUSTOMER_CONCURRENCY_ERROR:
                    return "Não foi possível salvar cliente, pois o mesmo já foi atualizado.";
                case DELETED_CUSTOMER_SUCCESS:
                    return "Cliente deletado com sucesso!";
                case DELETED_CUSTOMER_ERROR:
                    return "Não foi possível deletar cliente!";
                default:
                    return null;
            }            
        }
    }
}