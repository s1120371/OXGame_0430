namespace 文字版OXGame
{
    namespace _0425
    {
        class Program
        {
            static void Main()
            {
                // 創建一個新的 OX 遊戲引擎
                OXGameEngine game = new OXGameEngine();
                // 運行遊戲
                game.RunGame();
            }
        }

        public class OXGameEngine
        {
            // 用於儲存遊戲標記的 3x3 棋盤 (9宮格)
            private char[,] gameMarkers;

            // 初始化遊戲引擎
            public OXGameEngine()
            {
                gameMarkers = new char[3, 3];
                ResetGame(); // 重置遊戲
            }

            // 設置玩家標記在指定位置
            public void SetMarker(int x, int y, char player)
            {
                if (IsValidMove(x, y))
                {
                    // 如果移動有效，設置標記
                    gameMarkers[x, y] = player;
                }
                else
                {
                    throw new ArgumentException("Invalid move!"); // 如果移動無效，拋出異常
                }
            }

            // 重置遊戲，讓棋盤上的所有位置都設置為空白
            public void ResetGame()
            {
                gameMarkers = new char[3, 3];
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        // 所有位置初始化為空格
                        gameMarkers[i, j] = ' ';
                    }
                }
            }

            // 檢查是沒有玩家獲勝
            public char IsWinner()
            {
                // 檢查橫向勝利
                for (int i = 0; i < 3; i++)
                {
                    if (gameMarkers[i, 0] != ' ' && gameMarkers[i, 0] == gameMarkers[i, 1] && gameMarkers[i, 1] == gameMarkers[i, 2])
                    {
                        return gameMarkers[i, 0];
                    }
                }

                // 檢查縱向勝利
                for (int j = 0; j < 3; j++)
                {
                    if (gameMarkers[0, j] != ' ' && gameMarkers[0, j] == gameMarkers[1, j] && gameMarkers[1, j] == gameMarkers[2, j])
                    {
                        return gameMarkers[0, j];
                    }
                }

                // 檢查對角線勝利
                if (gameMarkers[0, 0] != ' ' && gameMarkers[0, 0] == gameMarkers[1, 1] && gameMarkers[1, 1] == gameMarkers[2, 2])
                {
                    return gameMarkers[0, 0];
                }

                if (gameMarkers[0, 2] != ' ' && gameMarkers[0, 2] == gameMarkers[1, 1] && gameMarkers[1, 1] == gameMarkers[2, 0])
                {
                    return gameMarkers[0, 2];
                }

                // 沒有獲勝者
                return ' ';
            }

            // 驗證給的行和列是不是有效的移動
            private bool IsValidMove(int x, int y)
            {
                if (x < 0 || x >= 3 || y < 0 || y >= 3)
                {
                    return false; // 如果坐標不在範圍內，為無效移動
                }

                if (gameMarkers[x, y] != ' ')
                {
                    return false; // 如果位置已經被佔用，為無效移動
                }

                return true; // 否則為有效移動
            }

            // 獲取指定位置的標記
            public char GetMarker(int x, int y)
            {
                return gameMarkers[x, y];
            }

            // 檢查遊戲是不是平局
            public bool IsDraw()
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        // 如果還有空位則不為平局
                        if (gameMarkers[i, j] == ' ')
                        {
                            return false;
                        }
                    }
                }
                // 沒有空位，遊戲為平局
                return true;
            }

            // 顯示棋盤
            public void DisplayBoard()
            {
                for (int i = 0; i < 3; i++)
                {
                    Console.WriteLine($"{gameMarkers[i, 0]} | {gameMarkers[i, 1]} | {gameMarkers[i, 2]}");
                    if (i < 2)
                    {
                        Console.WriteLine("--+---+--");
                    }
                }
            }

            // 運行遊戲
            public void RunGame()
            {
                char currentPlayer = 'X';
                while (true)
                {
                    // 顯示棋盤
                    DisplayBoard();
                    Console.WriteLine($"玩家 {currentPlayer}，請輸入橫向和縱向位置 [0~2] (格式：列 行)：");

                    // 獲取玩家輸入
                    string input = Console.ReadLine();
                    string[] inputs = input.Split(' ');

                    int x, y;
                    // 解析玩家輸入
                    if (inputs.Length == 2 && int.TryParse(inputs[0], out x) && int.TryParse(inputs[1], out y))
                    {
                        try
                        {
                            // 設置玩家標記
                            SetMarker(x, y, currentPlayer);
                        }
                        catch (ArgumentException e)
                        {
                            Console.WriteLine(e.Message);
                            continue; // 如果移動無效，繼續遊戲
                        }
                    }
                    else
                    {
                        Console.WriteLine("無效輸入，請重新輸入。");
                        continue; // 如果輸入無效，繼續遊戲
                    }

                    // 檢查有沒有獲勝者
                    char winner = IsWinner();
                    if (winner != ' ')
                    {
                        DisplayBoard();
                        Console.WriteLine($"贏家為: {currentPlayer}");
                        break; // 遊戲結束
                    }

                    // 檢查是不是平局
                    if (IsDraw())
                    {
                        DisplayBoard();
                        Console.WriteLine("平局！");
                        break; // 遊戲結束
                    }

                    // 切換玩家
                    currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
                }
            }
        }
    }
}