# Vehicle Controls
A lightweight ScriptHookVDotNet plugin that improves vehicle realism in GTA V Story Mode.

## Features
- 🚗 Manual engine start/stop
- 🚪 Smart vehicle exit
  - Short press **F**: Exit while keeping the engine running.
  - Hold **F**: Turn off the engine before exiting.
- 🔄 Automatic turn indicators while steering
- 🌡️ Dynamic brake temperature simulation
- 🚨 Brake fade based on brake temperature
- 🔔 Immersive brake condition notifications
- ⚙️ Easy configuration through `Vehicle Controls.ini`

---

## Requirements
- Grand Theft Auto V
- ScriptHookV (latest - Alexander Blade)
- ScriptHookVDotNet (latest)

---

## Installation
1. Install ScriptHookV.
2. Install ScriptHookVDotNet.
3. Extract zip file and move `Vehicle Controls.dll` and `Vehicle Controls.ini` into:

```
Grand Theft Auto V/scripts/
```

---

## Brake Temperature System
Brake performance changes depending on temperature.

| Condition | Effect |
|-----------|--------|
| Good | Full braking performance |
| Warm | Slight temperature increase |
| Hot | Minor brake fade |
| Critical | Significant brake fade |
| Dangerous | Severe brake fade |
| Brake Failure Risk | Very limited braking |
| Failed | Almost no braking force |

---

## Automatic Indicators
Indicators automatically activate while steering at low speeds and cancel once the steering wheel returns near the center.

---

## Notes
- Works only when the player is driving.
- Compatible with Story Mode.

---

## Known Limitations
- Brake temperatures reset after changing vehicles.
- Does not modify NPC vehicles.

---

## Changelog
### v1.0

- Initial release
- Manual engine control
- Smart exit system
- Automatic indicators
- Brake temperature simulation
- Brake condition notifications
- INI configuration support

---

## License
This project is released for personal use and modification.
Please do not re-upload or redistribute modified versions without giving proper credit.

---

## Author
**DEV'Z**
If you enjoy this project, consider leaving a rating and feedback on GTA5-Mods.
