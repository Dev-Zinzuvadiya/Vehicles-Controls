using GTA;
using GTA.UI;
using GTA.Math;
using GTA.Native;
using GTA.NaturalMotion;
using Control = GTA.Control;
using System;
using System.Drawing;
using System.Windows.Forms;


public class Vehicle_Controls : Script
{
    public Vehicle_Controls()
    {
        Tick += OnTick;
        KeyUp += OnKeyUp;
        KeyDown += OnKeyDown;
        Aborted += OnAbort;
        Interval = 25;
    }



    //  =>> RUN EVERY FUNCTIONS AND CODE ON EVERY TICK  <<= //
    // =======================================================
    private void OnTick(object sender, EventArgs e)
    {
    }



    //  =>> EVENT AND FUNCTIONS RUN AFTER KEY RELEASED  <<= //
    // =======================================================
    private void OnKeyUp(object sender, KeyEventArgs e)
    {
    }



    //  =>> EVENT AND CODE RUN WHEN KEY PRESSED  <<= //
    // =======================================================
    private void OnKeyDown(object sender, KeyEventArgs e)
    {
    }



    //  =>> RESET EVERY FIELDS AFTER SCRIPT RELOADING  <<= //
    // =======================================================
    private void OnAbort(object sender, EventArgs e)
    {
    }
}
