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
    // <----- REQUIRED FIELDS FOR INI -----> //
    private ScriptSettings _config;
    private Keys _engineToggle = Keys.Space;
    private bool _AutoIndicators = true;
    private bool _brakeTemperature = true;
    private bool _brakeNotifications = true;



    // --------------------------------------------------
    // REQUIRED FIELDS FOR REALISTIC VEHICLE CONTROLS
    // --------------------------------------------------

    // <----- MANUAL ENGINE FIELD ----->
    private Vehicle _currentVehicle = null;
    private Vehicle _lastVehicle = null;
    private bool _initializedVehicle = false;
    private bool _currentEngineState = false;
    private bool _vehicleExitKeyHeld = false;
    private DateTime _vehicleExitKeyTime;


    // <----- INDICATORS AND HEADLIGHT CONTROLS FIELDS ----->
    private readonly bool[] _enableIndicator = new bool[2];
    private readonly DateTime[] _indicatorTime = new DateTime[2];

    // <<<-------------------------------------------->>> //



    public Vehicle_Controls()
    {
        Tick += OnTick;
        KeyUp += OnKeyUp;
        KeyDown += OnKeyDown;
        Aborted += OnAbort;
        Interval = 25;

        INI_CONFIG();
        Notification.Show("Vehicle-Controls is ~g~Activated");
    }



    //  =>> RUN EVERY FUNCTIONS AND CODE ON EVERY TICK  <<= //
    // =======================================================
    private void OnTick(object sender, EventArgs e)
    {
        try
        {
            Ped player = Game.Player.Character;
            if (player == null || !player.Exists() || player.IsDead) return;


            if (player.IsInVehicle()) _currentVehicle = player.CurrentVehicle;
            else _lastVehicle = player.LastVehicle;


            PROCESS_VEHICLE_CONTROLS(player, _currentVehicle, _lastVehicle);
        }
        catch (Exception ex) { Notification.Show($"~r~OnTick~w~: {ex.Message}"); }
    }



    //  =>> EVENT AND FUNCTIONS RUN AFTER KEY RELEASED  <<= //
    // =======================================================
    private void OnKeyUp(object sender, KeyEventArgs e)
    {
        try
        {
            Ped player = Game.Player.Character;
            bool VehicleStatus = !player.IsInVehicle() || _currentVehicle == null || _currentVehicle.GetPedOnSeat(VehicleSeat.Driver) != player;
            if (VehicleStatus || player.IsInHeli || player.IsInPlane) return;


            // --- CONTROL VEHICLE ENGINE VIA 'INI SET-UP KEY' --- //
            if (e.KeyCode == _engineToggle)
            {
                if (!_currentEngineState)       // --- START ENGINE IF ENGINE CURRENTLY OFF --- //
                {
                    _currentEngineState = true;
                    Function.Call(Hash.SET_VEHICLE_ENGINE_ON, _currentVehicle, true, false, true);
                }
                // --- TURN OFF ENGINE IF ENGINE CURRENTLY RUNNING --- //
                else
                {
                    _currentEngineState = false;
                    Function.Call(Hash.SET_VEHICLE_ENGINE_ON, _currentVehicle, false, false, true);
                }
            }


            // --- HANDLE VEHICLE EXIT EVENT WITH TIME(ms)  --- //
            if (e.KeyCode == Keys.F)
            {
                _vehicleExitKeyHeld = false;
                double holdingTime = (DateTime.Now - _vehicleExitKeyTime).TotalMilliseconds;    // CALCULATE HOW LONG F WAS HELD
                Notification.Show($"~b~TOTAL HELD-TIME~w~: {holdingTime:000}~g~(ms)");

                // --- HOLD 500ms ABOVE FOR ENGINE OFF AND VEHICLE EXIT --- //
                if (holdingTime >= 500)
                {
                    if (_currentVehicle.Speed > 10f) return;

                    if (_currentVehicle != null && _currentVehicle.Exists())
                    {
                        _currentVehicle.IsEngineRunning = false; player.Task.LeaveVehicle();
                    }
                }
                // --- LESS THAN 750ms LEAVE ENGINE ON --- //
                else
                {
                    if (_currentVehicle != null && _currentVehicle.Exists())
                    {
                        _currentVehicle.IsEngineRunning = true; player.Task.LeaveVehicle();
                    }
                }
            }
        }
        catch (Exception ex) { Notification.Show($"~r~OnKeyUp~w~: {ex.Message}"); }
    }



    //  =>> EVENT AND CODE RUN WHEN KEY PRESSED  <<= //
    // =======================================================
    private void OnKeyDown(object sender, KeyEventArgs e)
    {
        try
        {
            Ped player = Game.Player.Character;
            if (!player.IsInVehicle() || _currentVehicle == null || _currentVehicle.GetPedOnSeat(VehicleSeat.Driver) != player) return;

            // --- STORE TIME(ms) DURING HOLD 'F' KEY --- //
            if (e.KeyCode == Keys.F && !_vehicleExitKeyHeld)
            {
                _vehicleExitKeyHeld = true; _vehicleExitKeyTime = DateTime.Now;
            }
        }
        catch { }
    }



    //  =>> RESET EVERY FIELDS AFTER SCRIPT RELOADING  <<= //
    // =======================================================
    private void OnAbort(object sender, EventArgs e)
    {
    }





    //  =>> WRAP ALL VEHICLE FEATURES INTO ONE MAIN FUNCTION  <<= //
    // =======================================================
    private void PROCESS_VEHICLE_CONTROLS(Ped player, Vehicle curVeh, Vehicle lasVeh)
    {
        VEHICLE_ENGINE_MANUAL_FEATURE(player, curVeh);
    }


    //  =>> MAKE SOME VEHICLE FEATURES FOR ACCESSABLE  <<= //
    // =======================================================
    private void VEHICLE_ENGINE_MANUAL_FEATURE(Ped player, Vehicle vehicle)
    {
        if (!player.IsInVehicle() || vehicle == null || vehicle.GetPedOnSeat(VehicleSeat.Driver) != player)
        {
            _initializedVehicle = false;
            _currentEngineState = false; return;
        }

        // --- CHECK ENGINE STATE DURING ENTERING VEHICLE  --- //
        if (!_initializedVehicle)
        {
            _initializedVehicle = true;

            // CHECK CURRENT VEHICLE ENGINE STATE (in bool formate)
            _currentEngineState = vehicle.IsEngineRunning;
        }

        // --- IF ENGINE STATE FOUND OFF, SO FORCE TO STAY OFF (avoid automatic) --- //
        if (!_currentEngineState) Function.Call(Hash.SET_VEHICLE_ENGINE_ON, vehicle, false, false, true);
    }





    //  =>>  CHANGEABLE SCRIPT FEATURES FROM .INI  <<= //
    // =======================================================
    private void INI_CONFIG()
    {
        _config = ScriptSettings.Load("scripts/Vehicle Controls.ini");

        // --- KEYS --- //
        _engineToggle = _config.GetValue("Keys", "EngineToggle", Keys.Space);

        // --- FEATURES --- //
        _AutoIndicators = _config.GetValue("Features", "AutoIndicators", true);
        _brakeTemperature = _config.GetValue("Features", "BrakeTemperature", true);
        _brakeNotifications = _config.GetValue("Features", "BrakeNotifications", true);

        _config.Save();
    }
}
