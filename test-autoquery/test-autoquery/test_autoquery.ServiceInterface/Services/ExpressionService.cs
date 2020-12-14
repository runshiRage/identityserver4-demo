using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace test_autoquery.ServiceInterface.Services
{
    public class ExpressionService
    {
        public delegate void MessageEventHandle(object sender, MessageEventArg arg);

        public event MessageEventHandle MessageManager;

        public static void Invoke() 
        {
            //c# 仅支持单行lambda表达式
            Expression<Action<string>> actionExpression = n => Console.WriteLine(n);
            Expression<Func<int, bool>> funcExpression1 = (n) => n < 0;
            Expression<Func<int, int, bool>> funcExpression2 = (n, m) => n - m == 0;
            Func<int, int, int> func = (m, n) => m * n + 2;

            var action = actionExpression.Compile();
            var func1 = funcExpression1.Compile();
            var func2 = funcExpression2.Compile();
            var res = func.Invoke(12, 45);

            action("this is a action expression");
            Console.WriteLine(func1(12)); 
            Console.WriteLine(func2(12,45));


            //使用api创建一个表达式
            //通过 Expression 类创建表达式树
            // lambda：num => num == 0
            ParameterExpression pExpression = Expression.Parameter(typeof(int)); //参数：num
            ConstantExpression cExpression = Expression.Constant(0); //常量：0
            BinaryExpression bExpression = Expression.MakeBinary(ExpressionType.Equal, pExpression, cExpression); //表达式：num == 0
            Expression<Func<int, bool>> lambda = Expression.Lambda<Func<int, bool>>(bExpression, pExpression); //lambda 表达式：num => num == 0
            Console.WriteLine(lambda.Body);


            //解析表达式
            Expression<Func<int, bool>> funcExpression = num => num == 0;
            ParameterExpression p1Expression = funcExpression.Parameters[0]; //lambda 表达式参数
            BinaryExpression body = (BinaryExpression)funcExpression.Body; //lambda 表达式主体：num == 0
            Console.WriteLine($"解析：{p1Expression.Name} => {body.Left} {body.NodeType} {body.Right}");


        }

        public void SendMessage(string message) 
        {
            var messageEventArg = new MessageEventArg { Message = message};
            MessageManager.Invoke(this, messageEventArg);
        }

        public void ResponseMessage() 
        {
            
        }

    }

    public class MessageEventArg : EventArgs 
    {
        public string Message { get; set; }
    }



}
