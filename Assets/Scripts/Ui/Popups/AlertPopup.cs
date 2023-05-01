using System;
using DefaultNamespace.Ui;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Popups
{
    [Flags]
    public enum AlertPopupButton
    {
        Ok = 1,
        Cancel = 2,
        Close = 4
    }

    public class AlertPopupData : IPresenterData
    {
        public AlertPopupButton AlertButton = AlertPopupButton.Ok | AlertPopupButton.Close;
        public string Description;
        public string OkButtonText = "ok";
        public Action<AlertPopupButton> OnActionComplete;
    }

    public class AlertPopup : BasePresenterWithController<AlertPopupController>
    {
        [SerializeField] protected Button closeButton;
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private TextMeshProUGUI description;
        [SerializeField] private Button okButton;
        [SerializeField] private Button cancelButton;
        [SerializeField] private TextMeshProUGUI okButtonText;
        [SerializeField] private TextMeshProUGUI cancelButtonText;

        public override void Show()
        {
            base.Show();
            description.text = PresenterController.AlertPopupData.Description;
            okButtonText.text = PresenterController.AlertPopupData.OkButtonText;
            cancelButton.gameObject.SetActive(
                PresenterController.AlertPopupData.AlertButton.HasFlag(AlertPopupButton.Cancel));
            closeButton.gameObject.SetActive(
                PresenterController.AlertPopupData.AlertButton.HasFlag(AlertPopupButton.Close));
            okButton.gameObject.SetActive(
                PresenterController.AlertPopupData.AlertButton.HasFlag(AlertPopupButton.Ok));

            okButton.OnClickAsObservable().Subscribe(_ =>
            {
                PresenterController.AlertPopupData.OnActionComplete?.Invoke(AlertPopupButton.Ok);
            }).AddTo(Disposables);
            cancelButton.OnClickAsObservable().Subscribe(_ =>
            {
                PresenterController.AlertPopupData.OnActionComplete?.Invoke(AlertPopupButton.Cancel);
            }).AddTo(Disposables);
            closeButton.OnClickAsObservable().Subscribe(_ =>
            {
                PresenterController.AlertPopupData.OnActionComplete?.Invoke(AlertPopupButton.Close);
            }).AddTo(Disposables);
        }
    }
}