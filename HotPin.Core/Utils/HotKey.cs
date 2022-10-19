using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace HotPin
{
    [Flags]
    public enum HotKeyModifiers
    {
        Alt = 1,
        Control = 2,
        Shift = 4,
        Windows = 8
    }

    public struct HotKey : IComparable<HotKey>
    {
        public Keys Key;
        public HotKeyModifiers Modifiers;

        public HotKey(Keys key, HotKeyModifiers modifiers)
        {
            Key = key;
            Modifiers = modifiers;
        }

        public int CompareTo(HotKey other)
        {
            return (Key, Modifiers).CompareTo((other.Key, other.Modifiers));
        }

        public override int GetHashCode()
        {
            return (Key, Modifiers).GetHashCode();
        }
    }

    public class HotKeyEventArgs : EventArgs
    {
        public readonly HotKey HotKey;

        public HotKeyEventArgs(Keys key, HotKeyModifiers modifiers)
        {
            HotKey = new HotKey(key, modifiers);
        }

        public HotKeyEventArgs(IntPtr hotKeyParam)
        {
            uint param = (uint)hotKeyParam.ToInt64();
            HotKey = new HotKey((Keys)((param & 0xffff0000) >> 16), (HotKeyModifiers)(param & 0x0000ffff));
        }
    }

    public class HotKeyForm : Form
    {
        public event EventHandler<HotKeyEventArgs> HotKeyPressed;

        private Dictionary<HotKey, int> Registered = new Dictionary<HotKey, int>();

        private const uint NoRepeat = 0x4000;

        public void RegisterHotKey(HotKey hotKey)
        {
            if (Registered.ContainsKey(hotKey))
                return;
            int id = Interlocked.Increment(ref Id);
            Registered.Add(hotKey, id);
            RegisterHotKeyInternal(Handle, id, (uint)hotKey.Modifiers | NoRepeat, (uint)hotKey.Key);
        }

        public void UnregisterHotKey(HotKey hotKey)
        {
            if (Registered.TryGetValue(hotKey, out int id))
            {
                Registered.Remove(hotKey);
                UnRegisterHotKeyInternal(Handle, id);
            }
        }

        public void UnregisterAll()
        {
            foreach (int id in Registered.Values)
            {
                UnRegisterHotKeyInternal(Handle, id);
            }
            Registered.Clear();
        }

        // protected

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_HOTKEY)
            {
                HotKeyEventArgs e = new HotKeyEventArgs(m.LParam);
                HotKeyPressed?.Invoke(this, e);
            }

            base.WndProc(ref m);
        }

        // private

        private void RegisterHotKeyInternal(IntPtr hwnd, int id, uint modifiers, uint key)
        {
            User32RegisterHotKey(hwnd, id, modifiers, key);
        }

        private void UnRegisterHotKeyInternal(IntPtr hwnd, int id)
        {
            User32UnregisterHotKey(Handle, id);
        }

        // window user32

        private static int Id = 0;
        private const int WM_HOTKEY = 0x312;

        [DllImport("user32", EntryPoint = "RegisterHotKey", SetLastError = true)]
        private static extern bool User32RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32", EntryPoint = "UnregisterHotKey", SetLastError = true)]
        private static extern bool User32UnregisterHotKey(IntPtr hWnd, int id);

    }

}