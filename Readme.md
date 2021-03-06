# Ellipsoid Placer

![Ellipse](https://github.com/JacekWozniak12/unity-ellipsoid-placer/blob/master/Docs/Ellipse_h5_w8.PNG)
## Features
- Unity Editor Tool
- Creates ellipse using multiple methods, like based on constant angle change (prefered for circles), item length or gameobject (position, rotation and size) based. 
- Stores settings in scriptable object.
- Settings naming relative to its shape 
- Customize your prefab looks used to generate with custom material and color (applied only to first material slot).

## Documentation and tutorials used
- Documentation from Unity.
- [Freya Holmer's lecture about tools](https://www.youtube.com/watch?v=pZ45O2hg_30)
- [Approximating length of ellipse](https://www.youtube.com/watch?v=5nW3nJhBHL0)
- [Implementing ellipse](https://www.youtube.com/watch?v=mQKGRoV_jBc)

## Author
https://jacekwozniak12.github.io/

## Thanks
https://twitter.com/PolandballD 

## Versions
- 2022-06-02b
  - Code cleanup
  - Partial class into multiple class object
  - GUI got its own class representations with interface IDisplayGUI
  - Generic DropView with IDisplayGUI and IEventNotifier<T> interfaces
- 2022-05-30a
  - After minor changes and code cleanups.
  - 3 placement modes
  - Basic prefab usage
  - Basic renderer adjustment available from editor

