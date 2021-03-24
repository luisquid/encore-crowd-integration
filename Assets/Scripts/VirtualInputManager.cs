using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WindowsInput;

public class VirtualInputManager : MonoBehaviour
{

    public int timePressed;
    private float timeOnSides;
    private float timeUsed;

    /// <summary>
    /// Clase necesaria para llamar las propiedades de la librería de simulación de input
    /// </summary>
    InputSimulator ipSimulator;
    private void OnEnable()
    {
        ipSimulator = new InputSimulator();
        timeOnSides = timePressed / 2;
    }

    /// <summary>
    /// Las funciones que se definan llaman a la librería del input simulator de la forma descrita en ellas
    /// para que ejecute la "presión" u "oprimida" de las teclas
    /// Hay 3 tipos de presión en teclas de teclado:
    /// Press: Simula una única presión sobre la tecla, esta no es muy prolongada y solo tiene el tiempo suficiente para ser detectada
    ///         funciona bien en juegos que requieren solo que una tecla se presione cada tanto
    /// Down: Simula una presión constante sobre la tecla, es una presión casi perpetua que no finalizará hasta que no haya una simulación
    ///         de la tecla siendo liberada, funciona muy bien para juegos actuales que toman un "axis" como input
    /// Up: Simula la tecla siendo liberada de la presión, puede usarse para detectar que una tecla fue dejada de ser presionada
    /// 
    /// Las funciones pueden ser creadas a conveniencia o removidas para dar lugar a un mejor sistema de detección y simulación
    /// </summary>
    #region Funciones
    public void PressW()
    {
        ipSimulator.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.VK_W);
    }
    public void PressA()
    {
        ipSimulator.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.VK_A);
    }
    public void PressS()
    {
        ipSimulator.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.VK_S);
    }
    public void PressD()
    {
        ipSimulator.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.VK_D);
    }
    public void PressSpace()
    {
        ipSimulator.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.SPACE);
    }

    public void HoldW()
    {
        ipSimulator.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_W);
    }
    public void HoldA()
    {
        ipSimulator.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_A);
    }
    public void HoldS()
    {
        ipSimulator.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_S);
    }
    public void HoldD()
    {
        ipSimulator.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_D);
    }

    public void ClickDer()
    {
        ipSimulator.Mouse.RightButtonClick();
    }

    public void ClickIzq()
    {
        ipSimulator.Mouse.LeftButtonClick();
    }

    public void MoveMouse(int x, int y)
    {
        ipSimulator.Mouse.MoveMouseBy(x, y);
    }

    public void Escape()
    {
        ipSimulator.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.ESCAPE);
    }

    /// <summary>
    /// Esta función se encarga por medio de un parámetro de tipo string, desde el script messages
    /// llamar a la corrutina que asigna una tecla virtual al inputsimulator
    /// </summary>
    /// <param name="key"></param>
    public void holdkey(string key)
    {
        StartCoroutine(PressKeyOnTime(key));
    } 
    #endregion

    /// <summary>
    /// Esta corrutina funciona para simular una presión por un tiempo determinado en la tecla específica
    /// Puede dar lugar a un mejor sistema de detección en conjunto con el reconocimiento de comandos desde el script messages
    /// </summary>
    /// <param name="key">string que se compara dentro de la corrutina para asignar una tecla virtual a ser presionada</param>
    /// <returns></returns>
    IEnumerator PressKeyOnTime(string key)
    {
        WindowsInput.Native.VirtualKeyCode vkey = new WindowsInput.Native.VirtualKeyCode();
        switch(key)
        {
            case "w":
            vkey = WindowsInput.Native.VirtualKeyCode.VK_W;
            timeUsed = timePressed;
            break;
            case "a":
            vkey = WindowsInput.Native.VirtualKeyCode.VK_A;
            timeUsed = timeOnSides;
            break;
            case "s":
            vkey = WindowsInput.Native.VirtualKeyCode.VK_S;
            timeUsed = timePressed;
            break;
            case "d":
            vkey = WindowsInput.Native.VirtualKeyCode.VK_D;
            timeUsed = timeOnSides;
            break;
            case "space":
            vkey = WindowsInput.Native.VirtualKeyCode.VK_S;
            timeUsed = timePressed;
            break;
        }
        ipSimulator.Keyboard.KeyDown(vkey);
        yield return new WaitForSeconds(timeUsed);
        ipSimulator.Keyboard.KeyUp(vkey);
    }

}
 