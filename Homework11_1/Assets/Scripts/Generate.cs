using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class Generate : MonoBehaviour
{
    private static int  _countCube = 0;
    private float _yShift = -0.5f;
    private bool _changePossition = true;
    Rigidbody _rb;
   // private float _speed = 2f;
    private Vector3 _startPossition;
    Mesh mesh;
    GameObject test;
    [SerializeField] GameObject _firstObj;
    Vector3 _currentObjPossition;
    private float? _x;
    private float? _z;
    void Start()
    {
        // _startPossition = new Vector3(-5f, _yShift, -0.5f);
        //GetComponent<MeshFilter>().mesh = mesh = new Mesh();
       // _rb = GetComponent<Rigidbody>();
      
       CreateCube(_x, _z);
    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rb.velocity = new Vector3(0, 0, 0);

           
                CheckPossition();


            CreateCube(_x, _z);
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
           
        }
    }

    void CreateCube(float? _x, float? _z) 
    {
        test = new GameObject();
        test.AddComponent<MeshFilter>();
        test.AddComponent<MeshRenderer>();
        test.AddComponent<Rigidbody>();
        Material mat = new Material(Shader.Find("Specular"));
        test.GetComponent<MeshRenderer>().material = new Material(mat);
        _rb = test.GetComponent<Rigidbody>();
        _rb.useGravity = false;
        _yShift++;
        _countCube++;
        //  mesh = new Mesh();
        //  mesh.name = "Custom Cube";
        // mesh.Clear();
        test.GetComponent<MeshFilter>().mesh = mesh = new Mesh(); 
        mesh.vertices = GenerateVectices(_x, _z);
        mesh.triangles = GenerateTriangles();
        mesh.RecalculateNormals();
        if (_changePossition)
        {
            _startPossition = new Vector3(-5f, _yShift, -0.5f);
            Move(Vector3.right);
        }
        else
        {
            _startPossition = new Vector3(-0.5f, _yShift, 2.5f);
            Move(-Vector3.forward);
        }
        test.transform.position = _startPossition;
        
       
          //  ScaleObject( Mathf.Abs(scalesize));

       
        _changePossition = !_changePossition;
    }

    void ScaleObject(float scalesize) 
    {
        if (_changePossition)
        {
            var gg = test.transform.localScale;
            gg.z -= scalesize;
            test.transform.localScale = gg;
           
        }
        else
        {
           var gg = test.transform.localScale;
            gg.x -= scalesize;
            test.transform.localScale = gg;
        }
    

    }
    private void Move( Vector3 move)
    {
        _rb.velocity = move;
    }
    Vector3[] GenerateVectices(float? _x, float? _z)
    {
       
        //добавить глобальный x ; z
        return new Vector3[]
        {
            new Vector3(_x ?? 0.0f,0.0f,_z ?? 0.0f), //0
             new Vector3(_x ??0.0f,0.0f,_z ??1.0f), //1
              new Vector3(_x ??0.0f,1.0f,_z ?? 0.0f), //2  
               new Vector3(_x ??0.0f,1.0f,_z ??1.0f), //3
                new Vector3(_x ??1.0f,0.0f,_z ??0.0f), //4
                 new Vector3(_x ??1.0f,0.0f,_z ??1.0f), //5
                  new Vector3(_x ??1.0f,1.0f,_z ??0.0f), //6
                   new Vector3(_x ??1.0f,1.0f,_z ??1.0f), //7       
        };
    }
    int[] GenerateTriangles()
    {
        return new int[]
        {
            0,2,4,2,6,4,
            4,6,5,6,7,5,
            4,5,0,5,1,0,
            1,3,0,3,2,0,
            5,7,1,7,3,1,
            2,3,6,3,7,6,
        };
    }

    void CheckPossition() 
    {
        Vector3 firstposs = new Vector3();
        Vector3 delta = new Vector3();
        if (_countCube > 0)
        {
            firstposs = _currentObjPossition;
        }
        else
        {
            firstposs = _firstObj.transform.localPosition;
        }
        if (_changePossition)
        {
            _currentObjPossition = test.transform.position;
            delta = firstposs - _currentObjPossition.normalized;
            _x = -0.5f + delta.x;
        }
        else
        {
            _currentObjPossition = test.transform.position;
            delta = firstposs - _currentObjPossition.normalized;
            _z = -0.5f + delta.z;
        }
       
        CreateSlicedObject(delta , _x, _z);
    }
    void CreateSlicedObject(Vector3 delta, float? resultX, float? resultZ) 
    {
        var tt = resultX;// значение по x обхекьа который остается
        var a = 1 - resultZ;
        GameObject ostanetsja = new GameObject();
        Instantiate(test, _currentObjPossition, Quaternion.identity);
        //создать 2 объекта один останется,второй с ригид боду и графитацией должен упасть и уничножится через пару секунж
        //у оставшегося задрать его размер и передать в созжание нового объекта
        // var c = 
        //    var a = Instantiate(test.transform.localScale,_currentObjPossition, Quaternion.identity);
    }
}
