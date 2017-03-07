﻿using Microsoft.TemplateEngine.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Templates.Core.PostActions.Catalog
{
    public class AddItemToProjectPostAction : PostAction<IReadOnlyList<ICreationPath>>
    {
        public AddItemToProjectPostAction(GenShell shell, IReadOnlyList<ICreationPath> config) : base(shell, config)
        {
        }

        public override void Execute()
        {
            var itemsToAdd = _config
                                .Where(o => !string.IsNullOrWhiteSpace(o.Path))
                                .Select(o => Path.GetFullPath(Path.Combine(_shell.OutputPath, o.Path)))
                                .ToList();

            _shell.AddItems(itemsToAdd.ToArray());
        }
    }
}
