namespace OpenTK_test
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Game game = new Game(900, 900))//выбираем разрешение
            {
                game.Run();
            }
        }
    }
}
