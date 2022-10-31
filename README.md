# C# CPU and GPU temperature
[![npm version](https://img.shields.io/npm/v/bootstrap)](https://www.npmjs.com/package/bootstrap)
## 環形進度條
![temperature](https://github.com/AlanWu867/temperature-gadget/blob/main/img/%E5%9C%96%E7%89%871.png)
### XAML 
```C#
        <ed:Arc x:Name="circle_value" ArcThickness="12"
            ArcThicknessUnit="Pixel"
            EndAngle="0"
            Fill="White"
            Opacity = "0.6"
            StartAngle="180"
            Stretch="None"
            StrokeEndLineCap="Round"
            StrokeStartLineCap="Round" Margin="30,10,33,11" Height="100" Width="100" />
            
        <ed:Arc ArcThickness="12"
            ArcThicknessUnit="Pixel"     
            EndAngle="0"
            Fill="#101a26"
            Opacity = "0.6"
            StartAngle="360"
            Stretch="None"
            StrokeEndLineCap="Round"
            StrokeStartLineCap="Round" Margin="30,10,33,11" Height="100" Width="100" />
            

```
### C#
```C#
        private void set_gpu_value()
        {
            if (Convert.ToInt32(cpuvalue) > 50) 
            {
                circle_value.EndAngle = (Convert.ToInt32(cpuvalue) - 50)*3.6;
            }
            else
            {
                circle_value.EndAngle = (Convert.ToInt32(cpuvalue)) * 3.6 - 180;
            }
            cpu_label.Content = cpuvalue;
            if (Convert.ToInt32(gpuvalue) > 50)
            {
                circle_value_Copy.EndAngle = (Convert.ToInt32(gpuvalue) - 50) * 3.6;
            }
            else
            {
                circle_value_Copy.EndAngle = (Convert.ToInt32(gpuvalue)) * 3.6 - 180;
            }
            gpu_label.Content = gpuvalue;
        }
```

## trmperature
source --> [HardwareTemperature](https://github.com/crazymi/HardwareTemperature/blob/master/HardwareTemperature/Program.cs)
```C#
        public void UpdateText()
        {

            Computer computer = new Computer() { CPUEnabled = true, GPUEnabled = true };
            computer.Open();

            System.Timers.Timer timer = new System.Timers.Timer() { Enabled = true, Interval = 1000 };
            timer.Elapsed += delegate (object sender, ElapsedEventArgs e)
            {

                foreach (IHardware hardware in computer.Hardware)
                {
                    hardware.Update();

                    foreach (ISensor sensor in hardware.Sensors)
                    {

                        if (sensor.SensorType == SensorType.Temperature)
                        {

                            if (sensor.Name == "GPU Core")
                            {
                                gpuvalue = sensor.Value.ToString();

                            }
                            if (sensor.Name == "CPU Package")
                            {
                                cpuvalue = Convert.ToInt32(sensor.Value).ToString();
                            }

                            Dispatcher.Invoke((Action)delegate () { set_gpu_value(); });
                        }

                    }
                }
            };

        }
```
## 快捷鍵(ctrl+Q)
For more details, please refer to the link --> [Keyboard listener](https://gist.github.com/Ciantic/471698)
``` C#
void KListener_KeyDown(object sender, RawKeyEventArgs args)
{
    if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
    {
        if (args.Key.ToString()=="Q")
        {

        }
    }
}
```
## Extended Window Styles (點擊穿透)


| 常數        | 值   | Description
| --------   | -----  | -----
| WS_EX_TRANSPARENT |0x00000020L      | 在繪製相同執行緒所建立之視窗底下的同層級 (之前，不應該繪製視窗) 。 視窗會顯示透明，因為已經繪製基礎同層級視窗的位。若要在沒有這些限制的情況下達到透明度，請使用 SetWindowRgn 函式。  

[more..](https://learn.microsoft.com/zh-tw/windows/win32/winmsg/extended-window-styles)

<br>

```C#
        public static class WindowsServices 
        {
            const int WS_EX_TRANSPARENT = 0x00000020;
            const int GWL_EXSTYLE = (-20);

            [DllImport("user32.dll")]
            static extern int GetWindowLong(IntPtr hwnd, int index);

            [DllImport("user32.dll")]
            static extern int SetWindowLong(IntPtr hwnd, int index, int newStyle);

            public static void SetWindowExTransparent(IntPtr hwnd)     //點擊穿透
            {
                var extendedStyle = GetWindowLong(hwnd, GWL_EXSTYLE);
                SetWindowLong(hwnd, GWL_EXSTYLE, extendedStyle | WS_EX_TRANSPARENT);
            }
            public static void SetWindowExTransparent2(IntPtr hwnd)    //取消點擊穿透
            {
                var extendedStyle = GetWindowLong(hwnd, GWL_EXSTYLE);
                SetWindowLong(hwnd, GWL_EXSTYLE, extendedStyle & ~WS_EX_TRANSPARENT);
            }
        }
```
<br>

## Sponsorship and Support
<a href="https://paypal.me/BaLaG867?country.x=TW&locale.x=zh_TW" https://paypal.me/BaLaG867?country.x=TW&locale.x=zh_TW"><img src="https://github.com/AlanWu867/temperature-gadget/blob/main/img/%E5%9C%96%E7%89%871.png " width="100" height="30" align="middle" /></a>
