/////////////////////////////////////////////////////////////////////////////////////////////////////
//
// Audiokinetic Wwise generated include file. Do not edit.
//
/////////////////////////////////////////////////////////////////////////////////////////////////////

#ifndef __WWISE_IDS_H__
#define __WWISE_IDS_H__

#include <AK/SoundEngine/Common/AkTypes.h>

namespace AK
{
    namespace EVENTS
    {
        static const AkUniqueID ATMOSPHERESOUND = 1402148638U;
        static const AkUniqueID COLLECTIBLE = 3356756485U;
        static const AkUniqueID CRICKET = 3360608258U;
        static const AkUniqueID ENEMYEFFECTS = 2401307071U;
        static const AkUniqueID ENEMYWALK = 1103958200U;
        static const AkUniqueID FOOTSTEP = 1866025847U;
        static const AkUniqueID GAMEMUSIC = 1533192012U;
        static const AkUniqueID MENUMUSIC = 679636833U;
        static const AkUniqueID PICKUP = 3978245845U;
        static const AkUniqueID PLAYERSOUNDS = 1327972334U;
        static const AkUniqueID SINGTRIG = 2791896842U;
        static const AkUniqueID WIND = 1537061107U;
    } // namespace EVENTS

    namespace STATES
    {
        namespace GAMESTATE
        {
            static const AkUniqueID GROUP = 4091656514U;

            namespace STATE
            {
                static const AkUniqueID CALM = 3753286132U;
                static const AkUniqueID CHASE = 417490929U;
                static const AkUniqueID ESCAPE = 3169107872U;
                static const AkUniqueID LOSE = 221232726U;
                static const AkUniqueID WIN = 979765101U;
            } // namespace STATE
        } // namespace GAMESTATE

    } // namespace STATES

    namespace SWITCHES
    {
        namespace ENEMYSOUNDSWITCH
        {
            static const AkUniqueID GROUP = 531986296U;

            namespace SWITCH
            {
                static const AkUniqueID ATTACK = 180661997U;
                static const AkUniqueID EAT = 781390793U;
                static const AkUniqueID ROAR = 2110808665U;
                static const AkUniqueID SENSE = 4069948569U;
            } // namespace SWITCH
        } // namespace ENEMYSOUNDSWITCH

        namespace GAMEMUSICSWITCH
        {
            static const AkUniqueID GROUP = 897609808U;

            namespace SWITCH
            {
                static const AkUniqueID CALM = 3753286132U;
                static const AkUniqueID CHASE = 417490929U;
            } // namespace SWITCH
        } // namespace GAMEMUSICSWITCH

        namespace PLAYERSOUNDS
        {
            static const AkUniqueID GROUP = 1327972334U;

            namespace SWITCH
            {
                static const AkUniqueID DEATH = 779278001U;
                static const AkUniqueID FLASHLIGHT = 1167979795U;
            } // namespace SWITCH
        } // namespace PLAYERSOUNDS

        namespace SURFACE
        {
            static const AkUniqueID GROUP = 1834394558U;

            namespace SWITCH
            {
                static const AkUniqueID GRASS = 4248645337U;
                static const AkUniqueID GROUND = 2528658256U;
                static const AkUniqueID WATER = 2654748154U;
            } // namespace SWITCH
        } // namespace SURFACE

    } // namespace SWITCHES

    namespace GAME_PARAMETERS
    {
        static const AkUniqueID CRICKET_PITCH = 870193789U;
        static const AkUniqueID ENEMYSENSE = 456153899U;
        static const AkUniqueID OCCULUSIONLOWPASS = 3106680682U;
        static const AkUniqueID OCCULUSIONVOLUME = 3990645511U;
        static const AkUniqueID TIPTOE_COMPRESSOR = 3695639568U;
        static const AkUniqueID WALL_COMPRESSOR = 554378191U;
    } // namespace GAME_PARAMETERS

    namespace TRIGGERS
    {
        static const AkUniqueID PICKUPSTINGER = 385890149U;
    } // namespace TRIGGERS

    namespace BANKS
    {
        static const AkUniqueID INIT = 1355168291U;
        static const AkUniqueID MAIN = 3161908922U;
        static const AkUniqueID MENU = 2607556080U;
    } // namespace BANKS

    namespace BUSSES
    {
        static const AkUniqueID MASTER_AUDIO_BUS = 3803692087U;
    } // namespace BUSSES

    namespace AUX_BUSSES
    {
        static const AkUniqueID ENVIORNMENT_DEFAULT = 2510970592U;
        static const AkUniqueID ENVIORNMENT_DWELLING = 2892358497U;
        static const AkUniqueID ENVIRONMENT_CORRIDOR = 1673477693U;
        static const AkUniqueID ENVIRONMTN_ECHOCHAMBER = 3240049645U;
    } // namespace AUX_BUSSES

    namespace AUDIO_DEVICES
    {
        static const AkUniqueID NO_OUTPUT = 2317455096U;
        static const AkUniqueID SYSTEM = 3859886410U;
    } // namespace AUDIO_DEVICES

}// namespace AK

#endif // __WWISE_IDS_H__
