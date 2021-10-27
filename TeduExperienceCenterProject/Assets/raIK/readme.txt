Robotic Arm with Inverse Kinematics / Ver 1.0
===
Created by Toshiyuki Nakamura(Meuse Robotics Inc)

Robotic Arm with Inverse Kinematics is a 6 axis robotic arm simulator with inverse kinematics.

Usage is pretty simple, just move sliders to determine the position and rotation of the end effector.

-- Scripts --
- CalcIK : CalcIK calculates the joint angles of the robot using slider values in real time. This unity project uses the analytical approach to calculate the inverse kinematics.
 The support site
https://meuse.co.jp/unity/unity-arm-robot-inverse-kinematics-analytical-approach/
explains the details of the method.
- RotationScript : RotationScript rotates the arm sub-assembly around a joint if the rotation should be made.
- CameraScript : CameraScript controls the view.

-- To edit robot dimensions --
If you want to change the arm length, edit CalcIK to change the values of L1ÅcL6 and also edit Scale and Position of the relevant arms.
