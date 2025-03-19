//使用事件机制，模拟实现一个闹钟功能。
//闹钟可以有嘀嗒（Tick）事件和响铃（Alarm）两个事件。
//在闹钟走时时或者响铃时，在控制台显示提示信息。

using System;

class AlarmClock
{
    private DateTime alarmTime;
    private Timer timer;
    public event Action Tick;
    public event Action Alarm;

    public AlarmClock()
    {
        timer = new Timer(OnTick, null, Timeout.Infinite, 1000);  // 每秒触发一次
    }

    public void SetAlarmTime(int hour, int minute,int second)
    {
        alarmTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hour, minute, second);
    }

    public void Start()
    {
        if (alarmTime == default(DateTime))
        {
            Console.WriteLine("set time first");
            return;
        }

        Console.WriteLine("clock starts");
        timer.Change(0, 1000);
    }

    public void Stop()
    {
        timer.Change(Timeout.Infinite, Timeout.Infinite);  // 停止定时器
        Console.WriteLine("stop...");
    }

    private void OnTick(object state)
    {
        Tick?.Invoke();  

        CheckAlarm();
    }
    private void CheckAlarm()
    {
        if (DateTime.Now.Hour == alarmTime.Hour && 
            DateTime.Now.Minute == alarmTime.Minute&& 
            DateTime.Now.Second == alarmTime.Second)
        {
            Alarm?.Invoke(); 
            Stop();
        }
    }
}

class Program
{
    static void Main()
    {
        AlarmClock alarmClock = new AlarmClock();

        // 监听tick事件
        alarmClock.Tick += () =>
        {
            Console.WriteLine("tick");
        };

        // 监听alarm事件
        alarmClock.Alarm += () =>
        {
            Console.WriteLine("alarm");
        };

        DateTime currentTime = DateTime.Now;
        // 设置alarmtime
        alarmClock.SetAlarmTime(currentTime.Hour, currentTime.Minute,currentTime.Second+10);
    
        alarmClock.Start();
        Console.WriteLine("press anykey to stop");
        Console.ReadKey();
        alarmClock.Stop();
    }
}