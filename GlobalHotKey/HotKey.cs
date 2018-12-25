/*用法:添加无参数方法 public void aa()
 * Ry.GlobalHotKey.GlobalHotKey h = new Ry.GlobalHotKey.GlobalHotKey();
 * 注册快捷键  h.Regist(this.Handle, modifiers, vk, CallBack);
 *             h.Regist(this.Handle, "CTRL+C", aaa);
 *  protected override void WndProc(ref Message m)
        {
            //窗口消息处理函数
            h.ProcessHotKey(m);
            base.WndProc(ref m);
        } 
 * 窗口关闭后注销 h.UnRegist(this.Handle, CallBack);
 *                h.UnRegist(this.Handle);
 *                
*/
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Ry.GlobalHotKey
{
    
    public class GlobalHotKey:Form
    {
        //引入系统API
        [DllImport("user32.dll")]
        static extern bool RegisterHotKey(IntPtr hWnd, int id, int modifiers, Keys vk);
        [DllImport("user32.dll")]
        static extern bool UnregisterHotKey(IntPtr hWnd, int id);



        protected class hwndCallbackPair
        {
            public IntPtr hWnd;
            public HotKeyCallBackHanlder callback;
        }

        public enum HotkeyModifiers
        {
            Alt = 1,
            Control = 2,
            Shift = 4,
            Win = 8
        }

        int keyid = 10;     //区分不同的快捷键
        Dictionary<int, hwndCallbackPair> keymap = new Dictionary<int, hwndCallbackPair>();   //每一个key对于一个处理函数
        public delegate void HotKeyCallBackHanlder();

        //组合控制键


        protected override void WndProc(ref Message m)
        {
            //窗口消息处理函数
            ProcessHotKey(m);
            base.WndProc(ref m);
        }

        //注册快捷键
        public void Regist(int modifiers, Keys vk, HotKeyCallBackHanlder callBack)
        {
            int id = keyid++;

            if (!RegisterHotKey(this.Handle, id, modifiers, vk))
                throw new Exception("注册失败！");
            hwndCallbackPair par = new hwndCallbackPair();
            par.hWnd = this.Handle;
            par.callback = callBack;
            keymap[id] = par;
        }

        //注册快捷键
        public void Regist(IntPtr hWnd, int modifiers, Keys vk, HotKeyCallBackHanlder callBack)
        {
            int id = keyid++;
            if (!RegisterHotKey(hWnd, id, modifiers, vk))
                throw new Exception("注册失败！");
            hwndCallbackPair par = new hwndCallbackPair();
            par.hWnd = hWnd;
            par.callback = callBack;
            keymap[id] = par;
        }

        //注册快捷键
        public void Regist(IntPtr hWnd, string controlAndKeys, HotKeyCallBackHanlder callBack)
        {
            if (controlAndKeys == "")
                return;
            int modifiers = 0;
            Keys vk = Keys.None;
            foreach (string value in controlAndKeys.Split('+'))
            {
                if (value.Trim().ToUpper() == "CTRL")
                    modifiers = modifiers + (int)GlobalHotKey.HotkeyModifiers.Control;
                else if (value.Trim().ToUpper() == "ALT")
                    modifiers = modifiers + (int)GlobalHotKey.HotkeyModifiers.Alt;
                else if (value.Trim().ToUpper() == "SHIFT")
                    modifiers = modifiers + (int)GlobalHotKey.HotkeyModifiers.Shift;
                else
                {
                    if (Regex.IsMatch(value, @"[0-9]"))
                    {
                        vk = (Keys)Enum.Parse(typeof(Keys), "D" + value.Trim());
                    }
                    else
                    {
                        vk = (Keys)Enum.Parse(typeof(Keys), value.Trim());
                    }
                }
            }
            Regist(hWnd, modifiers, vk, callBack);
        }

        // 注销快捷键
        public void UnRegist(IntPtr hWnd, HotKeyCallBackHanlder callBack)
        {
            foreach (KeyValuePair<int, hwndCallbackPair> var in keymap)
            {
                if (var.Value.callback == callBack)
                {
                    UnregisterHotKey(hWnd, var.Key);
                    return;
                }
            }
        }

        public void UnRegist(IntPtr hWnd)
        {
            foreach (KeyValuePair<int, hwndCallbackPair> var in keymap)
            {
                if (var.Value.hWnd == hWnd)
                {
                    UnregisterHotKey(hWnd, var.Key);
                    return;
                }
            }
        }

        public void UnRegist()
        {
            foreach (KeyValuePair<int, hwndCallbackPair> var in keymap)
            {
                if (var.Value.hWnd == this.Handle)
                {
                    UnregisterHotKey(this.Handle, var.Key);
                    return;
                }
            }
        }

        // 快捷键消息处理
        public void ProcessHotKey(Message m)
        {
            if (m.Msg == 0x312)
            {
                int id = m.WParam.ToInt32();
                hwndCallbackPair cp;
                if (keymap.TryGetValue(id, out cp))
                    cp.callback();
            }
        }
    }
}
