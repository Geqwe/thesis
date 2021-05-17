using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PickupItem : MonoBehaviour
{
    public static PickupItem instance;
    public bool canPickUp = false;

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
    AudioSource source;

    void Awake()
    {
        instance = this;
        if (level == 1)
            InitFirstDictionary();
        else
            InitSecondDictionary();

        source = GetComponent<AudioSource>();
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
        Item toInsert = new Item("That's a switch. A switch is a hardware device that connects multiple devices, on a wired computer network, creating a LAN.", clips[0]);
        items.Add("Switch", toInsert);
        toInsert = ChangeItem("That's a router. A router is responsible for sending and receiving (routing) data between networks. Most routers today work as Access Points too.", clips[1], toInsert);
        items.Add("Router", toInsert);
        toInsert = ChangeItem("That's an access point. An Access Point does the same job as the Switch, connecting devices creating a LAN, however with wireless connection with the devices. Wireless connectivity happens through electromagnetic waves. Most routers today work as access points too.", clips[2], toInsert);
        items.Add("Access", toInsert);
        toInsert = ChangeItem("That's a Network Interface Card (NIC). An NIC is a component inside the computer that enables wired and wireless network connection.", clips[3], toInsert);
        items.Add("NetCard", toInsert);
    }

    Item ChangeItem(string str, AudioClip clip, Item toIns)
    {
        toIns.description = str;
        toIns.clip = clip;
        return toIns;
    }

    public void GetItem(string itemName)
    {
        if (!canPickUp)
            return;

        Item itemPicked = items[itemName];
        adviceText.text = itemPicked.description;

        source.Stop();
        source.clip = itemPicked.clip;
        source.Play();

        if(!itemPicked.visited)
        {
            itemPicked.visited = true;
            allVisited++;
            if(allVisited==items.Count)
            {
                btn2.SetActive(true);
                canPickUp = false;
                StartCoroutine(ShowButton());
            }
        }
    }

    IEnumerator ShowButton()
    {
        yield return new WaitForSeconds(13f);
        GetComponent<DialogueTrigger>().enabled = true;
    }
}
