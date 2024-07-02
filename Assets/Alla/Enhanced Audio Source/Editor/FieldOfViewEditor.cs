// Enhanced Audio Source
// Copyright (C) 2022 Nick Harrison

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CustomEditor (typeof(SoundSource))]
[CanEditMultipleObjects]
public class FieldOfViewEditor : Editor{

#if UNITY_2020_2_OR_NEWER

    [DrawGizmo(GizmoType.Selected | GizmoType.InSelectionHierarchy)]

    static void DrawHandles(SoundSource dS, GizmoType gizmoType)
    {
        Vector3 position = Vector3.zero;
        Vector3 handleSize = new Vector3(5f, 5f, 5f);
        float handleSizeScreen;
        handleSizeScreen = HandleUtility.GetHandleSize(handleSize) / 8;
        Vector3 test = new Vector3(0f, 0f, 90f);

        //Angles Setup
        Vector3 startAngleA = dS.Direction(-dS.onAxisAngle / 2);
        Vector3 startAngleB = dS.Direction(dS.onAxisAngle / 2);
        Vector3 startAngleC = dS.DirectionVert((-dS.onAxisAngle) / 2);
        Vector3 startAngleD = dS.DirectionVert((dS.onAxisAngle) / 2);
        Vector3 endAngleA = dS.Direction(-dS.offAxisAngle / 2);
        Vector3 endAngleB = dS.Direction(dS.offAxisAngle / 2);
        Vector3 endAngleC = dS.DirectionVert(-dS.offAxisAngle / 2);
        Vector3 endAngleD = dS.DirectionVert(dS.offAxisAngle / 2);

        Handles.matrix = dS.transform.localToWorldMatrix;

        Handles.zTest = UnityEngine.Rendering.CompareFunction.LessEqual;

        Handles.color = Color.white;
        if (dS.drawLine)
            Handles.DrawLine(position, dS.listener.transform.position - dS.transform.position, dS.lineThickness);

        if (dS.viewHorizontal)
        {
            //On Axis Lines
            Handles.color = dS.onAxisColor;
            dS.onAxisColor.a = dS.volume;
            Handles.DrawLine(position, position + (startAngleA * dS.maxDistance), dS.lineThickness);
            Handles.DrawLine(position, position + (startAngleB * dS.maxDistance), dS.lineThickness);

            //On Axis Arc 
            Handles.color = dS.colorOnAxisFill;
            dS.colorOnAxisFill.a = dS.volume * dS.OnOffAxisFill;
            Handles.DrawSolidArc(position, -Vector3.up, Vector3.forward, dS.onAxisAngle / 2, dS.maxDistance);
            Handles.DrawSolidArc(position, Vector3.up, Vector3.forward, dS.onAxisAngle / 2, dS.maxDistance);

            //Off Axis Lines
            Handles.color = dS.offAxisColor;
            dS.offAxisColor.a = dS.volume;
            Handles.DrawLine(position, position + (endAngleA * dS.maxDistance), dS.lineThickness);
            Handles.DrawLine(position, position + (endAngleB * dS.maxDistance), dS.lineThickness);

            //Off Axis Arc
            Handles.color = dS.colorOffAxisFill;
            dS.colorOffAxisFill.a = dS.volume * dS.OnOffAxisFill;
            Handles.DrawSolidArc(position, -Vector3.up, -Vector3.forward, -dS.offAxisAngle / 2 + 180, dS.maxDistance);
            Handles.DrawSolidArc(position, Vector3.up, -Vector3.forward, -dS.offAxisAngle / 2 + 180, dS.maxDistance);
        }    

        if (dS.viewVertical)
        {
            //On Axis Lines Vertical
            Handles.color = dS.onAxisColor;
            dS.onAxisColor.a = dS.volume;
            Handles.DrawLine(position, position + (startAngleC * dS.maxDistance), dS.lineThickness);
            Handles.DrawLine(position, position + (startAngleD * dS.maxDistance), dS.lineThickness);

            //Vertical On Axis Arc 
            Handles.color = dS.colorOnAxisFill;
            dS.colorOnAxisFill.a = dS.volume * dS.OnOffAxisFill;
            Handles.DrawSolidArc(position, -Vector3.left, Vector3.forward, dS.onAxisAngle / 2, dS.maxDistance);
            Handles.DrawSolidArc(position, Vector3.left, Vector3.forward, dS.onAxisAngle / 2, dS.maxDistance);

            //Off Axis Lines Vertical
            Handles.color = dS.offAxisColor;
            dS.offAxisColor.a = dS.volume;
            Handles.DrawLine(position, position + (endAngleC * dS.maxDistance), dS.lineThickness);
            Handles.DrawLine(position, position + (endAngleD * dS.maxDistance), dS.lineThickness);

            //Vertical Off Axis Arc
            Handles.color = dS.colorOffAxisFill;
            dS.colorOffAxisFill.a = dS.volume * dS.OnOffAxisFill;
            Handles.DrawSolidArc(position, -Vector3.left, -Vector3.forward, -dS.offAxisAngle / 2 + 180, dS.maxDistance);
            Handles.DrawSolidArc(position, Vector3.left, -Vector3.forward, -dS.offAxisAngle / 2 + 180, dS.maxDistance);
        }

        //Max Distance Horizontal Disc
        Handles.color = dS.colorMaxRadius;
        dS.colorMaxRadius.a = dS.volume;
        Handles.DrawWireDisc(position, Vector3.up, dS.maxDistance, dS.lineThickness);

        if (dS.viewVertical){
            //Max Distance Secondary Vertical Disc
            Handles.color = dS.colorMaxRadius;
            dS.colorMaxRadius.a = dS.volume;
            Handles.DrawWireDisc(position, Vector3.left, dS.maxDistance, dS.lineThickness);
        }

        ////Max Distance Third Vertical Disc Option
        //Handles.color = dS.colorMaxRadius;
        //dS.colorMaxRadius.a = dS.volume;
        //Handles.DrawWireDisc(position, Vector3.forward, dS.maxDistance, dS.lineThickness);

        //Min Distance Horizontal Disc
        Handles.color = dS.colorMinRadius;
        dS.colorMinRadius.a = dS.volume;
        Handles.DrawWireDisc(position, Vector3.up, dS.minDistance, dS.lineThickness);

        //Resize Min & Max Distance Caps
        Handles.SphereHandleCap(0, Vector3.zero + new Vector3(dS.minDistance, 0f, 0f), Quaternion.identity, handleSizeScreen, EventType.Repaint);
        Handles.SphereHandleCap(0, Vector3.zero + new Vector3(-dS.minDistance, 0f, 0f), Quaternion.identity, handleSizeScreen, EventType.Repaint);
        Handles.color = dS.colorMaxRadius;
        Handles.SphereHandleCap(0, Vector3.zero + new Vector3(0f, 0f, dS.maxDistance), Quaternion.identity, handleSizeScreen, EventType.Repaint);
        Handles.SphereHandleCap(0, Vector3.zero + new Vector3(0f, 0f, -dS.maxDistance), Quaternion.identity, handleSizeScreen, EventType.Repaint);
        
        Handles.zTest = UnityEngine.Rendering.CompareFunction.Greater;
        Handles.color = dS.depthBehindColor;
        dS.depthBehindColor.a = dS.volume;

        //Max & Max Distance Horizontal Disc Behind Objects
        Handles.DrawWireDisc(position, Vector3.up, dS.maxDistance, dS.lineThickness);
        Handles.DrawWireDisc(position, Vector3.up, dS.minDistance, dS.lineThickness);

        //On & Off Axis Lines Behind Objects
        if (dS.viewHorizontal){
            Handles.DrawLine(position, position + (startAngleA * dS.maxDistance), dS.lineThickness);
            Handles.DrawLine(position, position + (startAngleB * dS.maxDistance), dS.lineThickness);
            Handles.DrawLine(position, position + (endAngleA * dS.maxDistance), dS.lineThickness);
            Handles.DrawLine(position, position + (endAngleB * dS.maxDistance), dS.lineThickness);
        }

        if (dS.viewVertical){
            Handles.DrawLine(position, position + (startAngleC * dS.maxDistance), dS.lineThickness);
            Handles.DrawLine(position, position + (startAngleD * dS.maxDistance), dS.lineThickness);
            Handles.DrawLine(position, position + (endAngleC * dS.maxDistance), dS.lineThickness);
            Handles.DrawLine(position, position + (endAngleD * dS.maxDistance), dS.lineThickness);
        }

        if (dS.drawLine)
            Handles.DrawLine(position, dS.listener.transform.position - dS.transform.position, dS.lineThickness);


        if (Camera.current.orthographic)
        {
            Vector3 normal = position - Handles.inverseMatrix.MultiplyVector(Camera.current.transform.forward);

            Handles.zTest = UnityEngine.Rendering.CompareFunction.LessEqual;

            //Max Distance Vertical Disc
            Handles.color = dS.colorMaxRadius;
            dS.colorMaxRadius.a = dS.volume;
            Handles.DrawWireDisc(position, normal, dS.maxDistance, dS.lineThickness);
           
            //Min Distance Vertical Disc
            Handles.color = dS.colorMinRadius;
            dS.colorMinRadius.a = dS.volume;
            Handles.DrawWireDisc(position, normal, dS.minDistance, dS.lineThickness);
          
            //Max Radius Fill Solid Disc
            Handles.color = dS.colorMaxRadiusFill;
            dS.colorMaxRadiusFill = dS.colorMaxRadius;
            dS.colorMaxRadiusFill.a = dS.volume * dS.maxSphereFill;
            Handles.DrawSolidDisc(position, normal, dS.maxDistance);

            //Min Radius Solid Fill Disc
            Handles.color = dS.colorMinRadiusFill;
            dS.colorMinRadiusFill = dS.colorMinRadius;
            dS.colorMinRadiusFill.a = dS.volume * dS.minSphereFill;
            Handles.DrawSolidDisc(position, normal, dS.minDistance);

            Handles.zTest = UnityEngine.Rendering.CompareFunction.Greater;
            Handles.color = dS.depthBehindColor;

            //Mix & Max Distance Vertical Discs Behind Objects
            Handles.DrawWireDisc(position, normal, dS.maxDistance, dS.lineThickness);
            Handles.DrawWireDisc(position, normal, dS.minDistance, dS.lineThickness);
          
        } else {

            Vector3 normal = position - Handles.inverseMatrix.MultiplyPoint(Camera.current.transform.position);

            Handles.zTest = UnityEngine.Rendering.CompareFunction.LessEqual;

            float sqrMagnitude = normal.sqrMagnitude;
            float maxNum0 = dS.maxDistance * dS.maxDistance;
            float maxNum1 = maxNum0 * maxNum0 / sqrMagnitude;
            float maxRadius = Mathf.Sqrt(maxNum0 - maxNum1);
            float minNum0 = dS.minDistance * dS.minDistance;
            float minNum1 = minNum0 * minNum0 / sqrMagnitude;
            float minRadius = Mathf.Sqrt(minNum0 - minNum1);


            //Max Distance Vertical Disc
            Handles.color = dS.colorMaxRadius;
            dS.colorMaxRadius.a = dS.volume;
            Handles.DrawWireDisc(position - maxNum0 * normal / sqrMagnitude, normal, maxRadius, dS.lineThickness);

            //Min Distance Vertical Disc
            Handles.color = dS.colorMinRadius;
            dS.colorMinRadius.a = dS.volume;
            Handles.DrawWireDisc(position - minNum0 * normal / sqrMagnitude, normal, minRadius, dS.lineThickness);

            //Max Radius Fill Solid Disc
            Handles.color = dS.colorMaxRadiusFill;
            dS.colorMaxRadiusFill = dS.colorMaxRadius;
            dS.colorMaxRadiusFill.a = dS.volume * dS.maxSphereFill;
            Handles.DrawSolidDisc(position - maxNum0 * normal / sqrMagnitude, normal, maxRadius);

            //Min Radius Solid Fill Disc
            Handles.color = dS.colorMinRadiusFill;
            dS.colorMinRadiusFill = dS.colorMinRadius;
            dS.colorMinRadiusFill.a = dS.volume * dS.minSphereFill;
            Handles.DrawSolidDisc(position - minNum0 * normal / sqrMagnitude, normal, minRadius);

            Handles.zTest = UnityEngine.Rendering.CompareFunction.Greater;
            Handles.color = dS.depthBehindColor;

            //Min & Max Distance Vertical Discs Behind Objects
            Handles.DrawWireDisc(position - maxNum0 * normal / sqrMagnitude, normal, maxRadius, dS.lineThickness);
            Handles.DrawWireDisc(position - minNum0 * normal / sqrMagnitude, normal, minRadius, dS.lineThickness);
        }
    }

#elif !UNITY_2020_2_OR_NEWER

    [DrawGizmo(GizmoType.NonSelected | GizmoType.InSelectionHierarchy)]

    static void DrawHandles(SoundSource dS, GizmoType gizmoType)
    {
        Vector3 position = Vector3.zero;
        Vector3 handleSize = new Vector3(5f, 5f, 5f);
        float handleSizeScreen;
        handleSizeScreen = HandleUtility.GetHandleSize(handleSize) / 8;
        Vector3 test = new Vector3(0f, 0f, 90f);

        //Angles Setup
        Vector3 startAngleA = dS.Direction(-dS.onAxisAngle / 2);
        Vector3 startAngleB = dS.Direction(dS.onAxisAngle / 2);
        Vector3 startAngleC = dS.DirectionVert((-dS.onAxisAngle) / 2);
        Vector3 startAngleD = dS.DirectionVert((dS.onAxisAngle) / 2);
        Vector3 endAngleA = dS.Direction(-dS.offAxisAngle / 2);
        Vector3 endAngleB = dS.Direction(dS.offAxisAngle / 2);
        Vector3 endAngleC = dS.DirectionVert(-dS.offAxisAngle / 2);
        Vector3 endAngleD = dS.DirectionVert(dS.offAxisAngle / 2);

        Handles.matrix = dS.transform.localToWorldMatrix;

        Handles.zTest = UnityEngine.Rendering.CompareFunction.LessEqual;

        Handles.color = Color.white;
        if (dS.drawLine)
            Handles.DrawLine(position, dS.listenerLocation.position - dS.transform.position);

        if (dS.viewHorizontal)
        {
            //On Axis Lines
            Handles.color = dS.onAxisColor;
            dS.onAxisColor.a = dS.volume;
            Handles.DrawLine(position, position + (startAngleA * dS.maxDistance));
            Handles.DrawLine(position, position + (startAngleB * dS.maxDistance));

            //On Axis Arc 
            Handles.color = dS.colorOnAxisFill;
            dS.colorOnAxisFill.a = dS.volume * dS.OnOffAxisFill;
            Handles.DrawSolidArc(position, -Vector3.up, Vector3.forward, dS.onAxisAngle / 2, dS.maxDistance);
            Handles.DrawSolidArc(position, Vector3.up, Vector3.forward, dS.onAxisAngle / 2, dS.maxDistance);

            //Off Axis Lines
            Handles.color = dS.offAxisColor;
            dS.offAxisColor.a = dS.volume;
            Handles.DrawLine(position, position + (endAngleA * dS.maxDistance));
            Handles.DrawLine(position, position + (endAngleB * dS.maxDistance));

            //Off Axis Arc
            Handles.color = dS.colorOffAxisFill;
            dS.colorOffAxisFill.a = dS.volume * dS.OnOffAxisFill;
            Handles.DrawSolidArc(position, -Vector3.up, -Vector3.forward, -dS.offAxisAngle / 2 + 180, dS.maxDistance);
            Handles.DrawSolidArc(position, Vector3.up, -Vector3.forward, -dS.offAxisAngle / 2 + 180, dS.maxDistance);
        }

        if (dS.viewVertical)
        {
            //On Axis Lines Vertical
            Handles.color = dS.onAxisColor;
            dS.onAxisColor.a = dS.volume;
            Handles.DrawLine(position, position + (startAngleC * dS.maxDistance));
            Handles.DrawLine(position, position + (startAngleD * dS.maxDistance));

            //Vertical On Axis Arc 
            Handles.color = dS.colorOnAxisFill;
            dS.colorOnAxisFill.a = dS.volume * dS.OnOffAxisFill;
            Handles.DrawSolidArc(position, -Vector3.left, Vector3.forward, dS.onAxisAngle / 2, dS.maxDistance);
            Handles.DrawSolidArc(position, Vector3.left, Vector3.forward, dS.onAxisAngle / 2, dS.maxDistance);

            //Off Axis Lines Vertical
            Handles.color = dS.offAxisColor;
            dS.offAxisColor.a = dS.volume;
            Handles.DrawLine(position, position + (endAngleC * dS.maxDistance));
            Handles.DrawLine(position, position + (endAngleD * dS.maxDistance));

            //Vertical Off Axis Arc
            Handles.color = dS.colorOffAxisFill;
            dS.colorOffAxisFill.a = dS.volume * dS.OnOffAxisFill;
            Handles.DrawSolidArc(position, -Vector3.left, -Vector3.forward, -dS.offAxisAngle / 2 + 180, dS.maxDistance);
            Handles.DrawSolidArc(position, Vector3.left, -Vector3.forward, -dS.offAxisAngle / 2 + 180, dS.maxDistance);
        }

        //Max Distance Horizontal Disc
        Handles.color = dS.colorMaxRadius;
        dS.colorMaxRadius.a = dS.volume;
        Handles.DrawWireDisc(position, Vector3.up, dS.maxDistance);

        if (dS.viewVertical)
        {
            //Max Distance Secondary Vertical Disc
            Handles.color = dS.colorMaxRadius;
            dS.colorMaxRadius.a = dS.volume;
            Handles.DrawWireDisc(position, Vector3.left, dS.maxDistance);
        }

        ////Max Distance Third Vertical Disc Option
        //Handles.color = dS.colorMaxRadius;
        //dS.colorMaxRadius.a = dS.volume;
        //Handles.DrawWireDisc(position, Vector3.forward, dS.maxDistance);

        //Min Distance Horizontal Disc
        Handles.color = dS.colorMinRadius;
        dS.colorMinRadius.a = dS.volume;
        Handles.DrawWireDisc(position, Vector3.up, dS.minDistance);

        //Resize Min & Max Distance Caps
        Handles.SphereHandleCap(0, Vector3.zero + new Vector3(dS.minDistance, 0f, 0f), Quaternion.identity, handleSizeScreen, EventType.Repaint);
        Handles.SphereHandleCap(0, Vector3.zero + new Vector3(-dS.minDistance, 0f, 0f), Quaternion.identity, handleSizeScreen, EventType.Repaint);
        Handles.color = dS.colorMaxRadius;
        Handles.SphereHandleCap(0, Vector3.zero + new Vector3(0f, 0f, dS.maxDistance), Quaternion.identity, handleSizeScreen, EventType.Repaint);
        Handles.SphereHandleCap(0, Vector3.zero + new Vector3(0f, 0f, -dS.maxDistance), Quaternion.identity, handleSizeScreen, EventType.Repaint);

        Handles.zTest = UnityEngine.Rendering.CompareFunction.Greater;
        Handles.color = dS.depthBehindColor;
        dS.depthBehindColor.a = dS.volume;

        //Max & Max Distance Horizontal Disc Behind Objects
        Handles.DrawWireDisc(position, Vector3.up, dS.maxDistance);
        Handles.DrawWireDisc(position, Vector3.up, dS.minDistance);

        //On & Off Axis Lines Behind Objects
        if (dS.viewHorizontal)
        {
            Handles.DrawLine(position, position + (startAngleA * dS.maxDistance));
            Handles.DrawLine(position, position + (startAngleB * dS.maxDistance));
            Handles.DrawLine(position, position + (endAngleA * dS.maxDistance));
            Handles.DrawLine(position, position + (endAngleB * dS.maxDistance));
        }

        if (dS.viewVertical)
        {
            Handles.DrawLine(position, position + (startAngleC * dS.maxDistance));
            Handles.DrawLine(position, position + (startAngleD * dS.maxDistance));
            Handles.DrawLine(position, position + (endAngleC * dS.maxDistance));
            Handles.DrawLine(position, position + (endAngleD * dS.maxDistance));
        }

        if (dS.drawLine)
            Handles.DrawLine(position, dS.listenerLocation.position - dS.transform.position);


        if (Camera.current.orthographic)
        {
            Vector3 normal = position - Handles.inverseMatrix.MultiplyVector(Camera.current.transform.forward);

            Handles.zTest = UnityEngine.Rendering.CompareFunction.LessEqual;

            //Max Distance Vertical Disc
            Handles.color = dS.colorMaxRadius;
            dS.colorMaxRadius.a = dS.volume;
            Handles.DrawWireDisc(position, normal, dS.maxDistance);

            //Min Distance Vertical Disc
            Handles.color = dS.colorMinRadius;
            dS.colorMinRadius.a = dS.volume;
            Handles.DrawWireDisc(position, normal, dS.minDistance);

            //Max Radius Fill Solid Disc
            Handles.color = dS.colorMaxRadiusFill;
            dS.colorMaxRadiusFill = dS.colorMaxRadius;
            dS.colorMaxRadiusFill.a = dS.volume * dS.maxSphereFill;
            Handles.DrawSolidDisc(position, normal, dS.maxDistance);

            //Min Radius Solid Fill Disc
            Handles.color = dS.colorMinRadiusFill;
            dS.colorMinRadiusFill = dS.colorMinRadius;
            dS.colorMinRadiusFill.a = dS.volume * dS.minSphereFill;
            Handles.DrawSolidDisc(position, normal, dS.minDistance);

            Handles.zTest = UnityEngine.Rendering.CompareFunction.Greater;
            Handles.color = dS.depthBehindColor;

            //Mix & Max Distance Vertical Discs Behind Objects
            Handles.DrawWireDisc(position, normal, dS.maxDistance);
            Handles.DrawWireDisc(position, normal, dS.minDistance);

        }
        else
        {

            Vector3 normal = position - Handles.inverseMatrix.MultiplyPoint(Camera.current.transform.position);

            Handles.zTest = UnityEngine.Rendering.CompareFunction.LessEqual;

            float sqrMagnitude = normal.sqrMagnitude;
            float maxNum0 = dS.maxDistance * dS.maxDistance;
            float maxNum1 = maxNum0 * maxNum0 / sqrMagnitude;
            float maxRadius = Mathf.Sqrt(maxNum0 - maxNum1);
            float minNum0 = dS.minDistance * dS.minDistance;
            float minNum1 = minNum0 * minNum0 / sqrMagnitude;
            float minRadius = Mathf.Sqrt(minNum0 - minNum1);


            //Max Distance Vertical Disc
            Handles.color = dS.colorMaxRadius;
            dS.colorMaxRadius.a = dS.volume;
            Handles.DrawWireDisc(position - maxNum0 * normal / sqrMagnitude, normal, maxRadius);

            //Min Distance Vertical Disc
            Handles.color = dS.colorMinRadius;
            dS.colorMinRadius.a = dS.volume;
            Handles.DrawWireDisc(position - minNum0 * normal / sqrMagnitude, normal, minRadius);

            //Max Radius Fill Solid Disc
            Handles.color = dS.colorMaxRadiusFill;
            dS.colorMaxRadiusFill = dS.colorMaxRadius;
            dS.colorMaxRadiusFill.a = dS.volume * dS.maxSphereFill;
            Handles.DrawSolidDisc(position - maxNum0 * normal / sqrMagnitude, normal, maxRadius);

            //Min Radius Solid Fill Disc
            Handles.color = dS.colorMinRadiusFill;
            dS.colorMinRadiusFill = dS.colorMinRadius;
            dS.colorMinRadiusFill.a = dS.volume * dS.minSphereFill;
            Handles.DrawSolidDisc(position - minNum0 * normal / sqrMagnitude, normal, minRadius);

            Handles.zTest = UnityEngine.Rendering.CompareFunction.Greater;
            Handles.color = dS.depthBehindColor;

            //Min & Max Distance Vertical Discs Behind Objects
            Handles.DrawWireDisc(position - maxNum0 * normal / sqrMagnitude, normal, maxRadius);
            Handles.DrawWireDisc(position - minNum0 * normal / sqrMagnitude, normal, minRadius);
        }
    }









#endif

    void OnSceneGUI(){
        SoundSource dS = (SoundSource)target;
        Vector3 transformPosition = dS.transform.position;

        //Max Wire Sphere
        Handles.color = dS.colorMaxWireSphere;
        dS.colorMaxWireSphere.a = dS.volume * dS.handlesVisibility;
        dS.maxDistance = Handles.RadiusHandle(Quaternion.identity, transformPosition, dS.maxDistance);

        //Min Wire Sphere
        Handles.color = dS.colorMinWireSphere;
        dS.colorMinWireSphere.a = dS.volume * dS.handlesVisibility;
        dS.minDistance = Handles.RadiusHandle(Quaternion.identity, transformPosition, dS.minDistance);
    }
}