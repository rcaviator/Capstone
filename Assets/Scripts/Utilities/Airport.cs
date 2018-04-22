using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Airport
{
    #region Fields

    

    #endregion

    #region Constructor

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="name">The name of the airport</param>
    /// <param name="nextName">The next airport's name</param>
    /// <param name="missionBriefing">The mission briefing</param>
    /// <param name="map">Map to the next airport</param>
    /// <param name="weather">Weather briefing</param>
    public Airport(string name, string nextName, string missionBriefing, Image map, string weather)
    {
        AirportName = name;
        NextAirportName = nextName;
        MissionBriefing = missionBriefing;
        Map = map;
        WeatherBriefing = weather;
    }

    #endregion

    #region Properties

    /// <summary>
    /// The airport name
    /// </summary>
    public string AirportName
    { get; private set; }

    /// <summary>
    /// The next airport's name
    /// </summary>
    public string NextAirportName
    { get; private set; }

    /// <summary>
    /// The mission briefing text for the next flight
    /// </summary>
    public string MissionBriefing
    { get; private set; }

    /// <summary>
    /// The map of the flight path to the next airport
    /// </summary>
    public Image Map
    { get; private set; }

    /// <summary>
    /// The weather briefing text for the next flight
    /// </summary>
    public string WeatherBriefing
    { get; private set; }

    #endregion

    #region Methods



    #endregion
}