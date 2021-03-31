using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupItem : MonoBehaviour
{
    public int level;
    public AudioClip[] clips;
    public Text adviceText;
    public GameObject btn2;
    public struct Item
    {
        public bool visited;
        public string description;
        public AudioClip clip;
        public Item(string desc, AudioClip cli)
        {
            visited = false;
            description = desc;
            clip = cli;
        }
    }

    int allVisited = 0;

    Dictionary<string, Item> items = new Dictionary<string, Item>();

    // Start is called before the first frame update
    void Awake()
    {
        if (level == 1)
            InitFirstDictionary();
        else
            InitSecondDictionary();
    }

    void InitFirstDictionary()
    {
        Item toInsert = new Item("A computer case is the enclosure that contains most of the components of a personal computer.", clips[0]);
        items.Add("Case", toInsert);
        toInsert = ChangeItem("Random-Access Memory or RAM is a form of computer memory that can be read and changed in any order. The stored data are lost when the computer is shut down.", clips[1], toInsert);
        items.Add("RAM", toInsert);
        toInsert = ChangeItem("The Motherboard is the main circuit board of a computer and makes everything work together. Attached to it, one can find the CPU, memory RAM expansion slots and other components.", clips[2], toInsert);
        items.Add("Motherboard", toInsert);
        toInsert = ChangeItem("A Power Supply Unit (PSU) converts mains AC to low-voltage regulated DC power for the internal components of a computer. Basically, it supplies power to all components.", clips[3], toInsert);
        items.Add("PowerSupply", toInsert);
        toInsert = ChangeItem("A Hard Disk Drive or HDD is a data storage device, that stores data even after the computer is shut down.", clips[4], toInsert);
        items.Add("HDD", toInsert);
        toInsert = ChangeItem("Solid State Drive or SSD is a data storage device, similar to a hard disk drive (HDD), but a lot faster at reading and writing.", clips[5], toInsert);
        items.Add("SSD", toInsert);
        toInsert = ChangeItem("A Central Processing Unit (CPU) is the electronic circuitry within a computer that executes instructions that make up a computer program.", clips[6], toInsert);
        items.Add("CPU", toInsert);
        toInsert = ChangeItem("A Sound Card is an internal expansion card that provides input and output of audio signals to and from a computer under control of computer programs.", clips[7], toInsert);
        items.Add("Sound", toInsert);
        toInsert = ChangeItem("A Video Card is an expansion card which generates a feed of output images to a display device (such as a computer monitor).", clips[8], toInsert);
        items.Add("GPU", toInsert);
    }

    void InitSecondDictionary()
    {
        Item toInsert = new Item("A switch is a hardware device that connects multiple devices on a computer network.", clips[0]);
        items.Add("Switch", toInsert);
        toInsert = ChangeItem("A router is a hardware device which is used to connect a LAN with an internet connection. It is used to receive, analyze and forward the incoming packets to another network.", clips[1], toInsert);
        items.Add("Router", toInsert);
    }

    Item ChangeItem(string str, AudioClip clip, Item toIns)
    {
        toIns.description = str;
        toIns.clip = clip;
        return toIns;
    }

    public void GetItem(string itemName)
    {
        Item itemPicked = items[itemName];
        adviceText.text = itemPicked.description;
        //play clip
        if(!itemPicked.visited)
        {
            itemPicked.visited = true;
            allVisited++;
            if(allVisited==items.Count)
            {
                btn2.SetActive(true);
            }
        }
    }
}
