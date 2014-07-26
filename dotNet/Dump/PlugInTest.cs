using System;
using System.Collections.Generic;
using System.Linq;

namespace PluginTest
{
    public class PlugInManager
    {
        public static Guid A = new Guid("AA2FA64F-9B0D-472D-BA29-BE577A2B7167");
        public static Guid B = new Guid("BB20FC28-BA44-4948-9D78-069181556E93");
        public static Guid C = new Guid("CCA9C329-BA57-4DE5-A35D-124EB3254BDC");
        public static Guid D = new Guid("DD46D80A-A9D0-4449-96C8-7804C2A234DC");
        public static Guid E = new Guid("EEDB673E-4FE5-488B-90F3-ECC6160F4DDF");

        private List<ICalc> _plugins = new List<ICalc>();

        private Dictionary<Guid, List<Guid>> _after = new Dictionary<Guid, List<Guid>>();
        private Dictionary<Guid, List<Guid>> _blocks = new Dictionary<Guid, List<Guid>>();

        public PlugInManager()
        {
            Load(new Calc_D());
            Load(new Calc_A());
            Load(new Calc_B());
            Load(new Calc_C());
            Load(new Calc_E());
        }

        public void Load(ICalc plugin)
        {
            _plugins.Add(plugin);

            _after.Add(plugin.Id, new List<Guid>());
            _blocks.Add(plugin.Id, new List<Guid>());

            ValidateAll();
            UpdateList();
        }

        private void ValidateAll()
        {
            _plugins.ForEach(p => p.Validate(_plugins.Select(pl => pl.Id).ToList()));
        }

        private void UpdateList()
        {
            // TODO: Should just clear lists and refill?

            _plugins.Where(p => p.IsValid).ToList().ForEach(p =>
            {
                p.AddToOthersAfterSteps.ForEach(id => { AddToList(id, id, p.Id, _after); });

                p.MyAfterSteps.ForEach(id => { AddToList(id, p.Id, id, _after); });

                p.ThisBlocks.ForEach(id => { AddToList(id, p.Id, id, _blocks); });

                p.BlocksThis.ForEach(id => { AddToList(id, id, p.Id, _blocks); });
            });
        }

        private void AddToList(Guid checkId, Guid key, Guid toAdd, Dictionary<Guid, List<Guid>> collection)
        {
            var currentPlugIn = _plugins.FirstOrDefault(plug => plug.Id == checkId && plug.IsValid);

            if (currentPlugIn != null)
            {
                var current = collection.First(kvp => kvp.Key == key);
                if (!current.Value.Contains(toAdd))
                    current.Value.Add(toAdd);
            }
        }
    }

    public interface ICalc
    {
        Guid Id { get; set; }

        bool IsValid { get; set; }

        List<Guid> MyAfterSteps { get; set; }

        List<Guid> AddToOthersAfterSteps { get; set; }

        List<Guid> ThisBlocks { get; set; }

        List<Guid> BlocksThis { get; set; }

        void Calculate();

        void Validate(List<Guid> available);
    }

    public abstract class Base_Calc : ICalc
    {
        public Guid Id { get; set; }

        public bool IsValid { get; set; }

        public List<Guid> RequiresToFunction { get; set; }

        public List<Guid> MyAfterSteps { get; set; }

        public List<Guid> AddToOthersAfterSteps { get; set; }

        public List<Guid> ThisBlocks { get; set; }

        public List<Guid> BlocksThis { get; set; }

        public Base_Calc()
        {
            RequiresToFunction = new List<Guid>();
            AddToOthersAfterSteps = new List<Guid>();
            MyAfterSteps = new List<Guid>();
            ThisBlocks = new List<Guid>();
            BlocksThis = new List<Guid>();
        }

        public abstract void Calculate();

        public void Validate(List<Guid> available)
        {
            IsValid = true;

            foreach (var id in RequiresToFunction)
            {
                if (available.Contains(id) == false)
                {
                    IsValid = false;
                }
            }
        }
    }

    public class Calc_A : Base_Calc
    {
        public Calc_A()
        {
            Id = PlugInManager.A;
        }

        public override void Calculate()
        { }
    }

    public class Calc_B : Base_Calc
    {
        public Calc_B()
        {
            Id = PlugInManager.B;
            RequiresToFunction.Add(PlugInManager.A);

            AddToOthersAfterSteps.Add(PlugInManager.A);

            ThisBlocks.Add(PlugInManager.D);
        }

        public override void Calculate()
        { }
    }

    public class Calc_C : Base_Calc
    {
        public Calc_C()
        {
            Id = PlugInManager.C;
            RequiresToFunction.Add(PlugInManager.A);

            AddToOthersAfterSteps.Add(PlugInManager.A);
            AddToOthersAfterSteps.Add(PlugInManager.B);
        }

        public override void Calculate()
        { }
    }

    public class Calc_D : Base_Calc
    {
        public Calc_D()
        {
            Id = PlugInManager.D;
            RequiresToFunction.Add(PlugInManager.C);

            MyAfterSteps.Add(PlugInManager.C);

            AddToOthersAfterSteps.Add(PlugInManager.A);
            AddToOthersAfterSteps.Add(PlugInManager.C);
        }

        public override void Calculate()
        { }
    }

    public class Calc_E : Base_Calc
    {
        public Calc_E()
        {
            Id = PlugInManager.E;
            RequiresToFunction.Add(PlugInManager.A);
            RequiresToFunction.Add(PlugInManager.D);

            MyAfterSteps.Add(PlugInManager.C);

            AddToOthersAfterSteps.Add(PlugInManager.A);
            AddToOthersAfterSteps.Add(PlugInManager.D);

            BlocksThis.Add(PlugInManager.C);
        }

        public override void Calculate()
        { }
    }
}