
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KCoreKit
{
    
    public class PrinterTester : MonoBehaviour
    {
        [SerializeField] private string text;
 
        private Printer _printer;

        [Button("실행")]
        public void Run()
        {
            _printer = GetComponent<Printer>();
            _printer.Stop();
            _printer.PreLoad(text);
            _printer.Print(() => { Debug.Log("end"); });
        }
    }
}