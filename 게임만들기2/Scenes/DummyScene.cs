using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 게임만들기.Scenes
{
    public class DummyScene : Scene
    {
        // 씬에 들어올 때 한 번 호출됨
        public override void Enter()
        {
            // 지금은 비워둠
        }

        // 매 루프마다 입력 처리
        public override void Update()
        {
            // 아무 키나 누르면 종료
            Console.ReadKey(true);
        }

        // 화면 출력
        public override void Render()
        {
            Console.Clear();
            Console.WriteLine("=== Dummy Scene ===");
            Console.WriteLine("씬 전환이 성공했습니다!");
            Console.WriteLine("아무 키나 누르세요");
        }

        // 씬에서 나갈 때 한 번 호출됨
        public override void Exit()
        {
            // 지금은 비워둠
        }
    }
}
