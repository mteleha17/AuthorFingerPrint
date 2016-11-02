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
        private IItemStore<Text, ITextModel> _textStore;
        private IItemStore<Group, IGroupModel> _groupStore;
        private IModelFactory _modelFactory;

        public TextController(IItemStore<Text, ITextModel> textStore,
            IItemStore<Group, IGroupModel> groupStore,
            IModelFactory modelFactory)
        {
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

        public List<ITextViewModel> GetTextByAuthor(string author)
        {
            if (string.IsNullOrWhiteSpace(author))
            {
                throw new ArgumentException("Author must not be null or white space.");
            }
            return _textStore.GetMany(x => x.Author == author).Select(x => (ITextViewModel)x).ToList();
        }

        public void CreateText(string name, TextReader input, int length, string author = null)
        {
            var model = _modelFactory.GetTextModel(name, input, length);
            if (!string.IsNullOrWhiteSpace(author))
            {
                model.SetAuthor(author);
            }
            _textStore.Add(model);
        }

        public void DeleteText(ITextViewModel model)
        {
            _textStore.Delete((ITextModel)model);
        }

        public void UpdateText(ITextViewModel model, string name = null, string author = null, bool? quotesOn = null)
        {
            throw new NotImplementedException();
            //var updatedModel = (ITextModel)model;
            //if (!string.IsNullOrWhiteSpace(name))
            //{
            //    updatedModel.SetName(name);
            //}
            //if (!string.IsNullOrWhiteSpace(author))
            //{
            //    updatedModel.SetAuthor(author);
            //}
            //if (quotesOn != null)
            //{
            //    updatedModel.SetIncludeQuotes((bool)quotesOn);
            //}
            //_textStore.Modify(updatedModel);
        }
    }
}
