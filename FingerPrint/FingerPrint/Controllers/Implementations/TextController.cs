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
        private List<ITextViewModel> _temporaryDatabase;
        //private ITextStore _textStore;
        //private IGroupStore _groupStore;
        private IModelFactory _modelFactory;

        public TextController(ITextStore textStore,
            IGroupStore groupStore,
            IModelFactory modelFactory)
        {
            _temporaryDatabase = new List<ITextViewModel>();
            //_textStore = textStore;
            //_groupStore = groupStore;
            _modelFactory = modelFactory;
        }

        public ITextViewModel GetTextByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Text name must not be null or white space.");
            }
            //return _textStore.GetOne(x => x.Name == name);
            return _temporaryDatabase.FirstOrDefault(x => x.GetName() == name);
        }

        public List<ITextViewModel> GetTextByAuthor(string author)
        {
            if (string.IsNullOrWhiteSpace(author))
            {
                throw new ArgumentException("Author must not be null or white space.");
            }
            //return _textStore.GetMany(x => x.Author == author).Select(x => (ITextViewModel)x).ToList();
            return _temporaryDatabase.Where(x => x.GetAuthor() == author).Select(x => (ITextViewModel)x).ToList();
        }

        public ITextViewModel CreateText(string name, TextReader input, int length, string author = null)
        {
            ITextModel model = _modelFactory.GetTextModel(name, input, length);
            if (!string.IsNullOrWhiteSpace(author))
            {
                model.SetAuthor(author);
            }
            //_textStore.Add(model);
            _temporaryDatabase.Add(model);
            return model;
        }

        public void DeleteText(ITextViewModel model)
        {
            //_textStore.Delete((ITextModel)model);
            _temporaryDatabase.Remove(model);
        }

        public void UpdateText(ITextViewModel model, string name = null, string author = null, bool? includeQuotes = null)
        {
            var updatedModel = (ITextModel)model;
            if (!string.IsNullOrWhiteSpace(name))
            {
                updatedModel.SetName(name);
                //_textStore.ModifyName((ITextModel)model, name);
            }
            if (!string.IsNullOrWhiteSpace(author))
            {
                updatedModel.SetAuthor(author);
                //_textStore.ModifyAuthor((ITextModel)model, author);

            }
            if (includeQuotes != null)
            {
                updatedModel.SetIncludeQuotes((bool)includeQuotes);
                //_textStore.ModifyIncludeQuotes((ITextModel)model, (bool)includeQuotes);
            }
        }

        public List<ITextViewModel> GetAllTexts()
        {
            //return _textStore.GetMany(x => true).Select(x => (ITextViewModel)x).ToList();
            return _temporaryDatabase.Where(x => true).Select(x => (ITextViewModel)x).ToList();
        }
    }
}
