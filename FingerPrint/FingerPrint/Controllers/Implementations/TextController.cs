using FingerPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FingerPrint.Models.Interfaces.TypeInterfaces;
using System.IO;
using FingerPrint;
using FingerPrint.Stores;
using FingerPrint.Models.Interfaces;

namespace FingerPrint.Controllers.Implementations
{
    public class TextController : ITextController
    {
        //private List<ITextViewModel> _textTempDb;
        //private List<IGroupViewModel> _groupTempDb;
        private ITextStore _textStore;
        private IGroupStore _groupStore;
        private IModelFactory _modelFactory;

        public TextController(ITextStore textStore,
            IGroupStore groupStore,
            IModelFactory modelFactory)
        {
            //_textTempDb = textTempDb;
            //_groupTempDb = groupTempDb;
            _textStore = textStore;
            _groupStore = groupStore;
            _modelFactory = modelFactory;
        }

        public ITextViewModel GetTextByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Text name must not be null or white space.");
            }
            return _textStore.GetOne(x => x.Name == name);
        }

        public ITextViewModel CreateText(string name, TextReader input, int length, string author = null)
        {
            if (_textStore.Exists(x => x.Name == name) || _groupStore.Exists(x => x.Name == name))
            {
                throw new ArgumentException($"Cannot create text because another item in the database already has the name {name}.");
            }
            ITextModel model = _modelFactory.GetTextModel(name, input, length);
            if (!string.IsNullOrWhiteSpace(author))
            {
                model.SetAuthor(author);
            }
            _textStore.Add(model);
            return model;
        }

        public void DeleteText(string name)
        {
            ITextModel model = _textStore.GetOne(x => x.Name == name);
            if (model == null)
            {
                throw new ArgumentException($"Can't delete text named {name} since it doesn't exist.");
            }
            _textStore.Disassociate(model);
            _textStore.Delete(model);
        }

        public ITextModel UpdateText(string oldName, string newName = null, string author = null, bool? includeQuotes = null)
        {
            ITextModel model = _textStore.GetOne(x => x.Name == oldName);
            if (model == null)
            {
                throw new ArgumentException("Cannot update text because it does not exist.");
            }

            if (author != null)
            {
                if (string.IsNullOrWhiteSpace(author))
                {
                    throw new ArgumentException("Cannot update text's author to null or whitespace.");
                }
                _textStore.ModifyAuthor(model, author);
                model.SetAuthor(author);
            }
            if (includeQuotes != null)
            {
                _textStore.ModifyIncludeQuotes(model, (bool)includeQuotes);
                model.SetIncludeQuotes((bool)includeQuotes);
            }
            if (newName != null)
            {
                if (string.IsNullOrWhiteSpace(newName))
                {
                    throw new ArgumentException("Cannot update text's name to null or whitespace.");
                }
                if (_textStore.Exists(x => x.Name == newName) || _groupStore.Exists(x => x.Name == newName))
                {
                    throw new ArgumentException($"Cannot update text name because another item in the database already has the name {newName}.");
                }
                _textStore.ModifyName(model, newName);
                model.SetName(newName);
            }
            return model;
        }

        public List<ITextViewModel> GetAllTexts()
        {
            return _textStore.GetMany(x => true).Select(x => (ITextViewModel)x).ToList();
        }
    }
}
