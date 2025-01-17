module Venue

open Fable.Helpers.React
open Fable.Helpers.React.Props
open Fable.Core.JsInterop
open Fable.Core
open Fulma

[<AutoOpen>]
module Leaflet =
  type MapProp =
    | Center of float*float
    | Zoom of float
    | ClassName of string

  let map (props:MapProp list) children = 
    ofImport "Map" "react-leaflet" (keyValueList CaseRules.LowerFirst props) children

  type TileLayerProp =
  | Url of string
  | Attribution of string
  let tileLayer () =
    let props = [
      Url "https://{s}.tile.osm.org/{z}/{x}/{y}.png"
      Attribution "&copy; <a href=\"http://osm.org/copyright\">OpenStreetMap</a> contributors"
    ]
    ofImport "TileLayer" "react-leaflet" (keyValueList CaseRules.LowerFirst props) []

  type MarkerProp =
  | Position of float*float
  let marker (props:MarkerProp list) children =
    ofImport "Marker" "react-leaflet" (keyValueList CaseRules.LowerFirst props) children

  let popup children =
    ofImport "Popup" "react-leaflet" () children



//these are terminal components, they don't take anything.
let venue = 
  Tile.parent [Tile.IsVertical] [
    article [Class "tile is-child notification is-primary"] [
      p [Class "title"] [ str "Venue" ]
      Image.image [ Image.Is128x128 ] [
        img [Src "https://www.redhat.com/profiles/rh/themes/redhatdotcom/img/logo.svg"]
      ]
      strong [] [
        str "Capitol F#"
      ]
      str " is being held at " 
      a [ Href "https://www.redhat.com/en/about/videos/inside-red-hats-raleigh-headquarters"] [str "Red Hat global headquarters"]
      str " in downtown Raleigh North Carolina, just a few blocks from the Raleigh convention center."
      hr []
      span [Style [FontWeight "bold"]] [
        str "We are in the annex.  Right down the street from the main enterence.  Looking at the main enterance, turn left.and before u grt to the corner, the enterence is on the right"
      ]
    ]
  ]
let food = 
  Tile.parent [Tile.IsVertical] [
    article [Class "tile is-child notification"] [
      p [Class "title"] [ str "Food" ]
      p [] [
        // Dame's chicken and waffles
        // Is this the right place to mention limited availability?
        str "We'll have Chicken and Waffles as well as vegan and low carb options and there is a wide variety of choices within easy walking distance of the conference center."
      ]
    ]
  ]

let accomodation = 
  let hotel name link =
    Media.media [] [
      Media.left [] [
        i [Class "fas fa-hotel"] []
      ]
      Media.content [] [
        a [Href link] [
          str name
        ]
      ]
    ]

  let hotels = 
    p [] [
            hotel 
              "Raleigh Marriott City Center" 
              "https://www.google.com/maps/place/Raleigh+Marriott+City+Center/@35.774974,-78.6421814,17z/data=!4m5!3m4!1s0x89ac5f715804d045:0xf512109c25af5aea!8m2!3d35.7735791!4d-78.6399557"
            hotel 
              "Sheraton Raleigh"
              "https://www.google.com/maps/place/Sheraton+Raleigh+Hotel/@35.774974,-78.6421814,17z/data=!4m5!3m4!1s0x89ac5f717b90484d:0x91190ab641cc99a7!8m2!3d35.77497!4d-78.6399929"
            hotel 
              "Holiday Inn"
              "https://www.google.com/maps/place/Holiday+Inn+Raleigh+Downtown/@35.7769051,-78.6443635,16.25z/data=!4m5!3m4!1s0x89ac5f6f98fac369:0xebc5bc60334731e7!8m2!3d35.7810101!4d-78.644094"
          ]

  Tile.parent [Tile.IsVertical] [
    article [Class "tile is-child notification is-info"] [
      p [Class "title"] [
        str "Accomodation"
      ]
      p [Class "subtitle"] [
        str "There are several options within easy walking distance."
      ]
      hotels
    ]
  ]

let view =
  let pos = 35.77502, -78.63811
  div [] [
    Leaflet.map [ Center pos; Zoom 18.] [
      tileLayer ()
      marker [ Position pos ] [
        popup [
          div [Style [ FontWeight "bold"]][ str "Red Hat" ]
          div [] [str "100 East Davie Street"]
          div [] [str "Raleigh, North Carolina 27601"]
        ]
      ]
    ]
    Section.section [] [
      Tile.ancestor [] [
        Tile.tile [Tile.IsVertical; Tile.Size Tile.Is12] [
          Tile.tile [] [
            venue
            food
            accomodation
          ]
        ]
      ]
    ]
    // Alternative Food?
  ]
