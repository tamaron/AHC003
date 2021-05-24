#pragma warning disable CS0414
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using static Utility;
class Program
{
    static void Main() => new Problem().Solve();
}

public class Problem { 

    public void Solve()
    {
        //var sw = new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false };
        //Console.SetOut(sw);
        long testCase = 1;
        //testCase = ReadLong();
        Solver solver = new Solver();
        GridGraph.GetDirIndex.Add(Dir.UP, (-1, 0));
        GridGraph.GetDirIndex.Add(Dir.DOWN, (1, 0));
        GridGraph.GetDirIndex.Add(Dir.LEFT, (0, -1));
        GridGraph.GetDirIndex.Add(Dir.RIGHT, (0, 1));
        while (testCase-- > 0) solver.Solve();
    }
}
static public class Utility { 

    static public Dir Over(Dir dir)
    {
        if (dir == Dir.DOWN) return Dir.UP;
        if (dir == Dir.UP) return Dir.DOWN;
        if (dir == Dir.LEFT) return Dir.RIGHT;
        if (dir == Dir.RIGHT) return Dir.LEFT;
        throw new Exception();
    }
    public static string Read() => Console.ReadLine();
    public static void Read<E1>(out E1 e1)
    {
        var rd = Console.ReadLine().Split(' ');
        e1 = (E1)Convert.ChangeType(rd[0], typeof(E1));
    }
    public static void Read<E1, E2>(out E1 e1, out E2 e2)
    {
        var rd = Console.ReadLine().Split(' ');
        e1 = (E1)Convert.ChangeType(rd[0], typeof(E1));
        e2 = (E2)Convert.ChangeType(rd[1], typeof(E2));
    }
    public static void Read<E1, E2, E3>(out E1 e1, out E2 e2, out E3 e3)
    {
        var rd = Console.ReadLine().Split(' ');
        e1 = (E1)Convert.ChangeType(rd[0], typeof(E1));
        e2 = (E2)Convert.ChangeType(rd[1], typeof(E2));
        e3 = (E3)Convert.ChangeType(rd[2], typeof(E3));
    }
    public static void Read<E1, E2, E3, E4>(out E1 e1, out E2 e2, out E3 e3, out E4 e4)
    {
        var rd = Console.ReadLine().Split(' ');
        e1 = (E1)Convert.ChangeType(rd[0], typeof(E1));
        e2 = (E2)Convert.ChangeType(rd[1], typeof(E2));
        e3 = (E3)Convert.ChangeType(rd[2], typeof(E3));
        e4 = (E4)Convert.ChangeType(rd[3], typeof(E4));
    }
    public static void Read<E1, E2, E3, E4, E5>(out E1 e1, out E2 e2, out E3 e3, out E4 e4, out E5 e5)
    {
        var rd = Console.ReadLine().Split(' ');
        e1 = (E1)Convert.ChangeType(rd[0], typeof(E1));
        e2 = (E2)Convert.ChangeType(rd[1], typeof(E2));
        e3 = (E3)Convert.ChangeType(rd[2], typeof(E3));
        e4 = (E4)Convert.ChangeType(rd[3], typeof(E4));
        e5 = (E5)Convert.ChangeType(rd[4], typeof(E5));
    }
    public static string[] Reads() => Console.ReadLine().Split(' ');
    public static long ReadLong() => long.Parse(Console.ReadLine());
    public static long[] ReadLongs() => Console.ReadLine().Split(' ').Select(e => long.Parse(e)).ToArray();
    public static double ReadDouble() => double.Parse(Console.ReadLine());
    public static double[] ReadDoubles() => Console.ReadLine().Split(' ').Select(e => double.Parse(e)).ToArray();
    
    public static void Pt(dynamic e1) => Console.WriteLine(e1);
    public static void Pt(dynamic e1, dynamic e2) => Console.WriteLine($"{e1} {e2}");
    public static void Pt(dynamic e1, dynamic e2, dynamic e3) => Console.WriteLine($"{e1} {e2} {e3}");
    public static void Pt(dynamic e1, dynamic e2, dynamic e3, dynamic e4) => Console.WriteLine($"{e1} {e2} {e3} {e4}");
    public static void Enum(IEnumerable<long> array) =>
        Console.WriteLine(
            array
            .Aggregate(new StringBuilder(), (result, next) => result.Append($"{next} "))
            .ToString()
        );
    public static void Enum(IEnumerable<double> array) =>
    Console.WriteLine(
        array
        .Aggregate(new StringBuilder(), (result, next) => result.Append($"{next} "))
        .ToString()
    );
    public static void Enum(IEnumerable<string> array) =>
    Console.WriteLine(
        array
        .Aggregate(new StringBuilder(), (result, next) => result.Append($"{next} "))
        .ToString()
    );
    public static void Flush() => Console.Out.Flush();
    public static readonly long INF = 1L << 60;
}

public class PriorityQueue<T> where T : IComparable
{
    private IComparer<T> _comparer = null;
    private int _type = 0;

    private T[] _heap;
    private int _sz = 0;

    private int _count = 0;

    /// <summary>
    /// Priority Queue with custom comparer
    /// </summary>
    public PriorityQueue(int maxSize, IComparer<T> comparer)
    {
        _heap = new T[maxSize];
        _comparer = comparer;
    }

    /// <summary>
    /// Priority queue
    /// </summary>
    /// <param name="maxSize">max size</param>
    /// <param name="type">0: asc, 1:desc</param>
    public PriorityQueue(int maxSize, int type = 0)
    {
        _heap = new T[maxSize];
        _type = type;
    }

    private int Compare(T x, T y)
    {
        if (_comparer != null) return _comparer.Compare(x, y);
        return _type == 0 ? x.CompareTo(y) : y.CompareTo(x);
    }

    public void Push(T x)
    {
        _count++;

        //node number
        var i = _sz++;

        while (i > 0)
        {
            //parent node number
            var p = (i - 1) / 2;

            if (Compare(_heap[p], x) <= 0) break;

            _heap[i] = _heap[p];
            i = p;
        }

        _heap[i] = x;
    }

    public T Pop()
    {
        _count--;

        T ret = _heap[0];
        T x = _heap[--_sz];

        int i = 0;
        while (i * 2 + 1 < _sz)
        {
            //children
            int a = i * 2 + 1;
            int b = i * 2 + 2;

            if (b < _sz && Compare(_heap[b], _heap[a]) < 0) a = b;

            if (Compare(_heap[a], x) >= 0) break;

            _heap[i] = _heap[a];
            i = a;
        }

        _heap[i] = x;

        return ret;
    }

    public int Count()
    {
        return _count;
    }

    public T Peek()
    {
        return _heap[0];
    }

    public bool Contains(T x)
    {
        for (int i = 0; i < _sz; i++) if (x.Equals(_heap[i])) return true;
        return false;
    }

    public void Clear()
    {
        while (this.Count() > 0) this.Pop();
    }

    public IEnumerator<T> GetEnumerator()
    {
        var ret = new List<T>();

        while (this.Count() > 0)
        {
            ret.Add(this.Pop());
        }

        foreach (var r in ret)
        {
            this.Push(r);
            yield return r;
        }
    }

    public T[] ToArray()
    {
        T[] array = new T[_sz];
        int i = 0;

        foreach (var r in this)
        {
            array[i++] = r;
        }

        return array;
    }
}


public partial class Solver
{
    Stopwatch stopwatch = new Stopwatch();
    public Solver()
    {
        stopwatch.Start();
    }
    public long Time {
        get => stopwatch.ElapsedMilliseconds;
    }
}

public class GridGraph
{
    public static Dir[] DirList = { Dir.UP, Dir.DOWN, Dir.LEFT, Dir.RIGHT, };
    public static Dictionary<Dir, (int, int)> GetDirIndex = new Dictionary<Dir, (int, int)>();
    public int H { get; private set; }
    public int W { get; private set; }
    public Node[,] Nodes { get; private set; }

    Dictionary<Node, Node> _prev;
    Dictionary<Node, double> _distance;
    public GridGraph(int h, int w)
    {
        H = h;
        W = w;
        Nodes = new Node[H, W];
        for(int i = 0; i < H; i++)for(int j = 0; j < W; j++) Nodes[i, j] = new Node((i, j));

        for (int i = 0; i < H; i++)
        {
            for(int j = 0; j < W; j++)
            {
                foreach(var dir in GridGraph.DirList)
                {
                    (int dh, int dw) delta = GridGraph.GetDirIndex[dir];
                    if (Exist(i + delta.dh, j + delta.dw)) 
                        Nodes[i, j].Edges
                            .Add(dir, new Edge(Nodes[i, j], Nodes[i + delta.dh, j + delta.dw]));
                }
            }
        }
    }

    public bool Exist(int h, int w)
    {
        return (0 <= h && h < H && 0 <= w && w < W);
    }

    public void Dijkstra(Node start)
    {
        for(int i = 0; i < H; i++)
        {
            for(int j = 0; j < W; j++)
            {
                Nodes[i, j].Priority = INF;
            }
        }
        _distance = new Dictionary<Node, double>();
        _prev = new Dictionary<Node, Node>();
        PriorityQueue<Node> queue = new PriorityQueue<Node>(2000);
        start.Priority = 0;
        queue.Push(start);
        while(queue.Count() > 0)
        {
            var current = queue.Pop();
            if (_distance.ContainsKey(current)) continue;
            _distance.Add(current, current.Priority);
            foreach(var dir in DirList)
            {
                if (current.Edges.ContainsKey(dir)) {
                    var now = current.Edges[dir].To.Priority;
                    var candi = current.Priority + current.Edges[dir].GetCost(this);
                    if(candi < now)
                    {
                        _prev[current.Edges[dir].To] = current;
                        current.Edges[dir].To.Priority = candi;
                        queue.Push(current.Edges[dir].To);
                    }
                }
            }
        }
    }


    public List<Dir> GetPath(int sh, int sw, int gh, int gw)
    {
        List<Dir> dirs = new List<Dir>();
        Node cur = Nodes[gh, gw];
        while(_prev.ContainsKey(cur))
        {
            if (_prev[cur].Locate.h + 1 == cur.Locate.h) dirs.Add(Dir.DOWN);
            if (_prev[cur].Locate.h - 1 == cur.Locate.h) dirs.Add(Dir.UP);
            if (_prev[cur].Locate.w + 1 == cur.Locate.w) dirs.Add(Dir.RIGHT);
            if (_prev[cur].Locate.w - 1 == cur.Locate.w) dirs.Add(Dir.LEFT);
            cur = _prev[cur];
        }
        return dirs.Reverse<Dir>().ToList();
    }
    public string GetPathString(int sh, int sw, int gh, int gw)
    {
        List<Dir> dirs = new List<Dir>();
        Node cur = Nodes[gh, gw];
        while (_prev.ContainsKey(cur))
        {
            if (_prev[cur].Locate.h + 1 == cur.Locate.h) dirs.Add(Dir.DOWN);
            if (_prev[cur].Locate.h - 1 == cur.Locate.h) dirs.Add(Dir.UP);
            if (_prev[cur].Locate.w + 1 == cur.Locate.w) dirs.Add(Dir.RIGHT);
            if (_prev[cur].Locate.w - 1 == cur.Locate.w) dirs.Add(Dir.LEFT);
            cur = _prev[cur];
        }
        return dirs.ConvertAll<string>(e =>
        {
            if (e == Dir.DOWN) return "D";
            if (e == Dir.UP) return "U";
            if (e == Dir.RIGHT) return "R";
            if (e == Dir.LEFT) return "L";
            throw new Exception();
        })
        .Reverse<string>()
        .Aggregate(new StringBuilder(), (ret, nex) => ret.Append(nex)).ToString();
    }
}

public class Node : IComparable
{
    public static int Lazy = 1;
    public Node((int, int) loc)
    {
        Locate = loc;
    }

    public double Priority;
    public (int h, int w) Locate;
    public Dictionary<Dir, Edge> Edges = new Dictionary<Dir, Edge>();
    public int CompareTo(object other) => Math.Sign((this.Priority - (other as Node).Priority) * Lazy);
}

public class Edge
{
    static public double[] bias =
    {
        1, 0.81, 0.64, 0.49, 0.36, 0.25, 0.16, 0.09, 0.04, 0.01,
    };
    static public double ConfidenceCriteria = 900;
    public Node From;
    public Node To;
    public double Confidence = 1 / ConfidenceCriteria;
    public double Len = 5000.0 / 900;

    public Edge(Node from, Node to)
    {
        this.From = from;
        this.To = to;
    }

    public double GetCost(GridGraph g)
    {
        //double score = Len / Confidence;
        //return score;
        double confidenceSum = 0 ;
        double lenSum = 0;

        int check = 3;

        Dir dir = default;
        if (From.Locate.h + 1 == To.Locate.h) dir = Dir.DOWN;
        if (From.Locate.h - 1 == To.Locate.h) dir = Dir.UP;
        if (From.Locate.w + 1 == To.Locate.w) dir = Dir.RIGHT;
        if (From.Locate.w - 1 == To.Locate.w) dir = Dir.LEFT;
        int fixw = From.Locate.w;
        int fixh = From.Locate.h;
        if(dir == Dir.DOWN || dir == Dir.UP)
        {
            for(int h = fixh - check; h < fixh + check + 1; h++)
            {
                if (!g.Exist(h, fixw)) continue;
                if (h == 29) continue;
                confidenceSum += g.Nodes[h, fixw].Edges[Dir.DOWN].Confidence * bias[Math.Abs(h - From.Locate.h)];
                lenSum += g.Nodes[h, fixw].Edges[Dir.DOWN].Len * bias[Math.Abs(h - From.Locate.h)];
            }
        }
        else
        {
            for (int w = fixw - check; w < fixw + check + 1; w++)
            {
                if (!g.Exist(fixh, w)) continue;
                if (w == 29) continue;
                confidenceSum += g.Nodes[fixh, w].Edges[Dir.RIGHT].Confidence * bias[Math.Abs(w - From.Locate.w)];
                lenSum += g.Nodes[fixh, w].Edges[Dir.RIGHT].Len * bias[Math.Abs(w - From.Locate.w)];
            }
        }
        return lenSum / confidenceSum;
    }
}

public enum Dir
{
    UP,
    DOWN,
    LEFT,
    RIGHT
}

public partial class Solver
{
    const int queryNum = 1000;
    public static int sh, sw, gh, gw;
    public static int QueryNumber = 0;
    public double[] turnBonus = {
        1, 1, 1, 0.5, 0.25, 0.125, 0.0625, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
        , 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
        , 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
        , 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
    };
    public void Solve()
    {
        var g = new GridGraph(30, 30);
        for (QueryNumber = 0; QueryNumber < queryNum; QueryNumber++)
        {
            if (QueryNumber < 12) Node.Lazy = -1;
            else Node.Lazy = 1;
            Read(out sh, out sw, out gh, out gw);
            g.Dijkstra(g.Nodes[sh, sw]);
            string pathString = g.GetPathString(sh, sw, gh, gw);
            Pt(pathString);
            var path = g.GetPath(sh, sw, gh, gw);
            int turnTime = 1;
            for (int i = 0; i < path.Count - 1; i++)
            {
                if (path[i] != path[i + 1]) turnTime++;
            }
            Flush();
            double len = ReadDouble();
            Node current = g.Nodes[sh, sw];
            foreach(var dir in path)
            {
                double curConfi = 1.0 / path.Count + 0.001 * turnBonus[turnTime];
                Node to = current.Edges[dir].To;
                current.Edges[dir].Confidence += curConfi;
                current.Edges[dir].Len += len * curConfi * curConfi;
                to.Edges[Over(dir)].Confidence = current.Edges[dir].Confidence;
                to.Edges[Over(dir)].Len = current.Edges[dir].Len;
                current = to;
            }
        }
    }
}