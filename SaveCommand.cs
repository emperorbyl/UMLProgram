using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace UMLProgram
{
    public class SaveCommand : Command
    {
        private readonly string _filename;
        internal SaveCommand(params object[] commandParameters)
        {
            if (commandParameters.Length > 0)
                _filename = commandParameters[0] as string;
        }

        public override bool Execute()
        {
            StreamWriter writer = new StreamWriter(_filename);
            TargetDrawing?.SaveToStream(writer.BaseStream);
            writer.Close();

            return true;
        }

        internal override void Undo()
        {
            // Do nothing -- we don't want to delete the saved file.
        }

        internal override void Redo()
        {
            Execute();
        }
    }
}
