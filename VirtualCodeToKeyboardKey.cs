namespace TappedKeyDisplay
{
    internal class VirtualCodeToKeyboardKey
    {
        public string vc2key(int vc)
        {
            string key = "";
            switch (vc)
            {
                case 1:
                    key = "Left mouse button";
                    break;
                case 2:
                    key = "Right mouse button";
                    break;
                case 3:
                    key = "Control-break processing";
                    break;
                case 4:
                    key = "Middle mouse button";
                    break;
                case 5:
                    key = "X1 mouse button";
                    break;
                case 6:
                    key = "X2 mouse button";
                    break;
                case 7:
                    key = "Undefined";
                    break;
                case 8:
                    key = "Backspace";
                    break;
                case 9:
                    key = "Tab";
                    break;
                case 12:
                    key = "Clear";
                    break;
                case 13:
                    key = "Enter";
                    break;
                case 16:
                    key = "Shift";
                    break;
                case 17:
                    key = "Ctrl";
                    break;
                case 18:
                    key = "Alt";
                    break;
                case 19:
                    key = "Pause";
                    break;
                case 20:
                    key = "Caps Lock";
                    break;
                case 27:
                    key = "Esc";
                    break;
                case 32:
                    key = " ";
                    break;
                case 33:
                    key = "Page Up";
                    break;
                case 34:
                    key = "Page Down";
                    break;
                case 35:
                    key = "End";
                    break;
                case 36:
                    key = "Home";
                    break;
                case 37:
                    key = "←";
                    break;
                case 38:
                    key = "↑";
                    break;
                case 39:
                    key = "→";
                    break;
                case 40:
                    key = "↓";
                    break;
                case 41:
                    key = "Select";
                    break;
                case 42:
                    key = "Print";
                    break;
                case 43:
                    key = "Execute";
                    break;
                case 44:
                    key = "Print Screen";
                    break;
                case 45:
                    key = "Insert";
                    break;
                case 46:
                    key = "Delete";
                    break;
                case 47:
                    key = "Help";
                    break;
                case 48:
                    key = "0";
                    break;
                case 49:
                    key = "1";
                    break;
                case 50:
                    key = "2";
                    break;
                case 51:
                    key = "3";
                    break;
                case 52:
                    key = "4";
                    break;
                case 53:
                    key = "5";
                    break;
                case 54:
                    key = "6";
                    break;
                case 55:
                    key = "7";
                    break;
                case 56:
                    key = "8";
                    break;
                case 57:
                    key = "9";
                    break;
                case 65:
                    key = "A";
                    break;
                case 66:
                    key = "B";
                    break;
                case 67:
                    key = "C";
                    break;
                case 68:
                    key = "D";
                    break;
                case 69:
                    key = "E";
                    break;
                case 70:
                    key = "F";
                    break;
                case 71:
                    key = "G";
                    break;
                case 72:
                    key = "H";
                    break;
                case 73:
                    key = "I";
                    break;
                case 74:
                    key = "J";
                    break;
                case 75:
                    key = "K";
                    break;
                case 76:
                    key = "L";
                    break;
                case 77:
                    key = "M";
                    break;
                case 78:
                    key = "N";
                    break;
                case 79:
                    key = "O";
                    break;
                case 80:
                    key = "P";
                    break;
                case 81:
                    key = "Q";
                    break;
                case 82:
                    key = "R";
                    break;
                case 83:
                    key = "S";
                    break;
                case 84:
                    key = "T";
                    break;
                case 85:
                    key = "U";
                    break;
                case 86:
                    key = "V";
                    break;
                case 87:
                    key = "W";
                    break;
                case 88:
                    key = "X";
                    break;
                case 89:
                    key = "Y";
                    break;
                case 90:
                    key = "Z";
                    break;
                case 91:
                    key = "Left Windows";
                    break;
                case 92:
                    key = "Right Windows";
                    break;
                case 93:
                    key = "Applications";
                    break;
                case 95:
                    key = "Computer Sleep";
                    break;
                case 96:
                    key = "0";
                    break;
                case 97:
                    key = "1";
                    break;
                case 98:
                    key = "2";
                    break;
                case 99:
                    key = "3";
                    break;
                case 100:
                    key = "4";
                    break;
                case 101:
                    key = "5";
                    break;
                case 102:
                    key = "6";
                    break;
                case 103:
                    key = "7";
                    break;
                case 104:
                    key = "8";
                    break;
                case 105:
                    key = "9";
                    break;
                case 106:
                    key = "*";
                    break;
                case 107:
                    key = "+";
                    break;
                case 108:
                    key = "|";
                    break;
                case 109:
                    key = "-";
                    break;
                case 110:
                    key = ".";
                    break;
                case 111:
                    key = "/";
                    break;
                case 112:
                    key = "F1";
                    break;
                case 113:
                    key = "F2";
                    break;
                case 114:
                    key = "F3";
                    break;
                case 115:
                    key = "F4";
                    break;
                case 116:
                    key = "F5";
                    break;
                case 117:
                    key = "F6";
                    break;
                case 118:
                    key = "F7";
                    break;
                case 119:
                    key = "F8";
                    break;
                case 120:
                    key = "F9";
                    break;
                case 121:
                    key = "F10";
                    break;
                case 122:
                    key = "F11";
                    break;
                case 123:
                    key = "F12";
                    break;
                case 124:
                    key = "F13";
                    break;
                case 125:
                    key = "F14";
                    break;
                case 126:
                    key = "F15";
                    break;
                case 127:
                    key = "F16";
                    break;
                case 128:
                    key = "F17";
                    break;
                case 129:
                    key = "F18";
                    break;
                case 130:
                    key = "F19";
                    break;
                case 131:
                    key = "F20";
                    break;
                case 132:
                    key = "F21";
                    break;
                case 133:
                    key = "F22";
                    break;
                case 134:
                    key = "F23";
                    break;
                case 135:
                    key = "F24";
                    break;
                case 144:
                    key = "Num Lock";
                    break;
                case 145:
                    key = "Scroll Lock";
                    break;
                case 160:
                    key = "Left Shift";
                    break;
                case 161:
                    key = "Right Shift";
                    break;
                case 162:
                    key = "Left Control";
                    break;
                case 163:
                    key = "Right Control";
                    break;
                case 164:
                    key = "Left Alt";
                    break;
                case 165:
                    key = "Right Alt";
                    break;
                case 173:
                    key = "Volume Mute";
                    break;
                case 174:
                    key = "Volume Down";
                    break;
                case 175:
                    key = "Volume Up";
                    break;
                case 176:
                    key = "Next Track";
                    break;
                case 177:
                    key = "Previous Track";
                    break;
                case 178:
                    key = "Stop Media";
                    break;
                case 179:
                    key = "Play/Pause Media";
                    break;
                case 180:
                    key = "Start Mail";
                    break;
                case 181:
                    key = "Select Media";
                    break;
                case 182:
                    key = "Start Application 1";
                    break;
                case 183:
                    key = "Start Application 2";
                    break;
                case 186:
                    key = ";";
                    break;
                case 187:
                    key = "+";
                    break;
                case 188:
                    key = ",";
                    break;
                case 189:
                    key = "-";
                    break;
                case 190:
                    key = ".";
                    break;
                case 191:
                    key = "/";
                    break;
                case 192:
                    key = "~";
                    break;
                case 219:
                    key = "[";
                    break;
                case 220:
                    key = "\\";
                    break;
                case 221:
                    key = "]";
                    break;
                case 222:
                    key = "'";
                    break;
                case 246:
                    key = "Attn";
                    break;
                case 247:
                    key = "CrSel";
                    break;
                case 248:
                    key = "ExSel";
                    break;
                case 249:
                    key = "Erase EOF";
                    break;
                case 250:
                    key = "Play";
                    break;
                case 251:
                    key = "Zoom";
                    break;
                case 253:
                    key = "PA1";
                    break;
                case 254:
                    key = "Clear";
                    break;
                default:
                    key = vc.ToString();
                    break;
            }
            return key;
        }
    }
}
